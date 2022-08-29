using Aurigma;
using GroupDocs.Comparison;
using GroupDocs.Comparison.Options;
using ImageMagick;
using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.IO;

namespace ImgCmp
{
    public partial class Form1 : Form
    {
        string fname1, fname2;
        private IBitmapAnalyzer BitmapAnalyzer { get; set; }
        private IDifferenceLabeler Labeler { get; set; }
        private IBoundingBoxIdentifier BoundingBoxIdentifier { get; set; }
        private Color BoundingBoxColor { get; set; }
        private AnalyzerTypes AnalyzerType { get; set; }
        private double JustNoticeableDifference { get; set; }
        private LabelerTypes LabelerType { get; set; }
        private int DetectionPadding { get; set; }
        private BoundingBoxModes BoundingBoxMode { get; set; }
        private int BoundingBoxPadding { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            BitmapAnalyzer = BitmapAnalyzerFactory.Create(AnalyzerType, JustNoticeableDifference);
            Labeler = LabelerFactory.Create(LabelerType, DetectionPadding);
            BoundingBoxIdentifier = BoundingBoxIdentifierFactory.Create(BoundingBoxMode, BoundingBoxPadding);
            
            CompareOptions options = new CompareOptions();
            if (options.BoundingBoxPadding < 0) throw new ArgumentException("bounding box padding must be non-negative");
            if (options.DetectionPadding < 0) throw new ArgumentException("detection padding must be non-negative");

            LabelerType = options.Labeler;
            JustNoticeableDifference = options.JustNoticeableDifference;
            BoundingBoxColor = options.BoundingBoxColor;
            DetectionPadding = options.DetectionPadding;
            BoundingBoxPadding = options.BoundingBoxPadding;
            BoundingBoxMode = options.BoundingBoxMode;
            AnalyzerType = options.AnalyzerType;

        }
        public void ResizeImage(string inFileName, string outFileName, int width, int height)
        {
            using (Image image = Image.FromFile(inFileName))
            {
                new Bitmap(image, width, height).Save(outFileName);
            }
        }

        private void label_firstImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            File.Delete("1.jpg");
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Images";
            openFileDialog1.Filter = "All Images|*.jpg; *.bmp; *.png";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.ToString() != "")
            {
                fname1 = openFileDialog1.FileName.ToString();
                ResizeImage(fname1, "1.jpg", 400, 400);
                
                using (var img = new Bitmap("1.jpg"))
                {
                    pictureBox1.Image = new Bitmap(img);
                    img.Dispose();
                }

                this.label_firstImg.Text = fname1;
            }
        }

        private void label_secondImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            File.Delete("2.jpg");
            openFileDialog2.FileName = "";
            openFileDialog2.Title = "Images";
            openFileDialog2.Filter = "All Images|*.jpg; *.bmp; *.png";
            openFileDialog2.ShowDialog();
            if (openFileDialog2.FileName.ToString() != "")
            {
                fname2 = openFileDialog2.FileName.ToString();
                ResizeImage(fname2, "2.jpg", 400, 400);
                
                using (var img = new Bitmap("2.jpg"))
                {
                    pictureBox2.Image = new Bitmap(img);
                    img.Dispose();
                }

                this.label_secondImg.Text = fname2;
            }
        }

        private void btn_text_compare_Click(object sender, EventArgs e)
        {
            Byte[] bytes1 = File.ReadAllBytes(fname1);
            String file1 = Convert.ToBase64String(bytes1);
            Value.rtbstr1 = file1;

            Byte[] bytes2 = File.ReadAllBytes(fname2);
            String file2 = Convert.ToBase64String(bytes2);
            Value.rtbstr2 = file2;

            Form2 createForm = new Form2();
            createForm.ShowDialog();

        }

        private void btn_table_compare_Click(object sender, EventArgs e)
        {
            var directories1 = ImageMetadataReader.ReadMetadata(fname1);
            var directories2 = ImageMetadataReader.ReadMetadata(fname2);
            string detail1 = "", detail2 = "";

            // print out all metadata
            foreach (var directory1 in directories1)
                foreach (var tag in directory1.Tags)
                {
                    detail1 += $"{directory1.Name} - {tag.Name} = {tag.Description}\n";
                }
            foreach (var directory2 in directories2)
                foreach (var tag in directory2.Tags)
                {
                    detail2 += $"{directory2.Name} - {tag.Name} = {tag.Description}\n";
                }

            Value.rtbstr1 = detail1;
            Value.rtbstr2 = detail2;

            Form2 createForm = new Form2();
            createForm.ShowDialog();
        }

        private void btn_hex_compare_Click(object sender, EventArgs e)
        {
            Byte[] bytes1 = File.ReadAllBytes(fname1);
            string file1 = bytes1.Aggregate(new StringBuilder(), (sb, v) => sb.AppendFormat("{0:X2} ", v)).ToString();
            Value.rtbstr1 = file1;

            Byte[] bytes2 = File.ReadAllBytes(fname2);
            string file2 = bytes2.Aggregate(new StringBuilder(), (sb, v) => sb.AppendFormat("{0:X2} ", v)).ToString();
            Value.rtbstr2 = file2;

            Form2 createForm = new Form2();
            createForm.ShowDialog();
        }

        private void btn_image_gap_Click(object sender, EventArgs e)
        {
            Bitmap firstImage = new Bitmap("1.jpg");
            Bitmap secondImage = new Bitmap("2.jpg");

            if (firstImage == null) throw new ArgumentNullException("firstImage");
            if (secondImage == null) throw new ArgumentNullException("secondImage");
            if (firstImage.Width != secondImage.Width || firstImage.Height != secondImage.Height) throw new ArgumentException("Bitmaps must be the same size.");

            var differenceMap = BitmapAnalyzer.Analyze(firstImage, secondImage);
            var differenceLabels = Labeler.Label(differenceMap);
            var boundingBoxes = BoundingBoxIdentifier.CreateBoundingBoxes(differenceLabels);
            var differenceBitmap = CreateImageWithBoundingBoxes(secondImage, boundingBoxes);
            
            using (var img = new Bitmap(differenceBitmap))
            {
                pictureBox3.Image = new Bitmap(img);
                img.Dispose();
            }

            firstImage.Dispose();
            secondImage.Dispose();
            differenceBitmap.Dispose();
        }

        private void btn_picture_compare_Click(object sender, EventArgs e)
        {
            //using (Comparer comparer = new Comparer(fname1))
            //{
            //    CompareOptions options = new CompareOptions();
            //    options.GenerateSummaryPage = false;

            //    comparer.Add(fname2);
            //    comparer.Compare("3.jpg", options);
            //}

            //pictureBox3.Image = new Bitmap("3.jpg");

            File.Delete("test.jpg");

            MagickImage img1 = new MagickImage("1.jpg");
            MagickImage img2 = new MagickImage("2.jpg");

            var imgDiff = new MagickImage();

            img1.Compare(img2, new ErrorMetric(), imgDiff);
            imgDiff.Write(@"test.jpg");
            using (var img = new Bitmap("test.jpg"))
            {
                pictureBox3.Image = new Bitmap(img);
                img.Dispose();
            }
            img1.Dispose();
            img2.Dispose();
        }

        private void btn_image_overlap_Click(object sender, EventArgs e)
        {
            Image img1 = (Bitmap)Image.FromFile(@"1.jpg");
            Image img2 = (Bitmap)Image.FromFile(@"2.jpg");

            KVImage.ImageBlender ib = new KVImage.ImageBlender();
            Bitmap bmp = new Bitmap(img1, img1.Width, img1.Height);

            int nOp = (int)Enum.GetValues(typeof(KVImage.ImageBlender.BlendOperation)).GetValue(3);

            ib.BlendImages(bmp, 0, 0, bmp.Width, bmp.Height, img2, 0, 0, (KVImage.ImageBlender.BlendOperation)nOp);

            pictureBox3.Image = new Bitmap(bmp);

            bmp.Dispose();
            img1.Dispose();
            img2.Dispose();
        }

        private Bitmap CreateImageWithBoundingBoxes(Bitmap secondImage, IEnumerable<Rectangle> boundingBoxes)
        {
            var differenceBitmap = secondImage.Clone() as Bitmap;
            if (differenceBitmap == null) throw new Exception("Could not copy secondImage");

            var boundingRectangles = boundingBoxes.ToArray();
            if (boundingRectangles.Length == 0)
                return differenceBitmap;

            using (var g = Graphics.FromImage(differenceBitmap))
            {
                var pen = new Pen(BoundingBoxColor);
                foreach (var boundingRectangle in boundingRectangles)
                {
                    g.DrawRectangle(pen, boundingRectangle);
                }
            }
            return differenceBitmap;
        }

    }
}
