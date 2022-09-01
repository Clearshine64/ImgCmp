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

        int cropX;
        int cropY;
        int cropWidth;
        int cropHeight;
        int dragX;
        int dragY;
        public Pen cropPen;
        public DashStyle cropDashStyle = DashStyle.DashDot;
        Rectangle rect = new Rectangle(0, 0, 0, 0);
        bool isMouseDown = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox2.AllowDrop = true;

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

            File.Delete("1.jpg");
            File.Delete("2.jpg");
            File.Delete("test.jpg");

            cropPen = new Pen(Color.Black, 1);
            cropPen.DashStyle = DashStyle.DashDotDot;
            cropX = 0;
            cropY = 0;
            cropWidth = 400;
            cropHeight = 400;
            dragX = 0;
            dragY = 0;
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
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Please select image first");
                return;
            }

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
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Please select image first");
                return;
            }

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
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Please select image first");
                return;
            }

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
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Please select image first");
                return;
            }

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

            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Please select image first");
                return;
            }

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
            if (!File.Exists("1.jpg") || !File.Exists("2.jpg"))
            {
                MessageBox.Show("Please select image first");
                return;
            }

            Image img1 = (Bitmap)Image.FromFile("1.jpg");
            Image img2 = (Bitmap)Image.FromFile("2.jpg");

            KVImage.ImageBlender ib = new KVImage.ImageBlender();
            Bitmap bmp = new Bitmap(img1, pictureBox1.Width, pictureBox1.Height);

            int nOp = (int)Enum.GetValues(typeof(KVImage.ImageBlender.BlendOperation)).GetValue(3);

            ib.BlendImages(bmp, 0, 0, bmp.Width, bmp.Height, img2, 0, 0, dragX, dragY, (KVImage.ImageBlender.BlendOperation)nOp);

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
        
        private void pictureBox1_Paint()
        {
            if (File.Exists("1.jpg"))
            {
                Image img1 = (Bitmap)Image.FromFile(@"1.jpg");
                Bitmap bmp = new Bitmap(img1, img1.Width, img1.Height);
                pictureBox1.Image = new Bitmap(bmp);
                bmp.Dispose();
                img1.Dispose();
            }
            else pictureBox1.Image = null;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            cropX = e.X;
            cropY = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                cropHeight = e.Y - cropY;
                cropWidth = e.X - cropX;
                pictureBox1_Paint();
                pictureBox1.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            pictureBox1.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);
        }
        private void pictureBox2_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox picbox = (PictureBox)sender;
            Graphics g = picbox.CreateGraphics();
            g.DrawImage((Image)e.Data.GetData(DataFormats.Bitmap), new Point(dragX, dragY));
        }
        private void pictureBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox2_Paint()
        {
            if (File.Exists("2.jpg"))
            {
                Image img2 = (Bitmap)Image.FromFile(@"2.jpg");
                Bitmap bmp = new Bitmap(img2, img2.Width, img2.Height);
                pictureBox2.Image = new Bitmap(bmp);
                bmp.Dispose();
                img2.Dispose();
            }
            else pictureBox2.Image = null;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            cropX = e.X;
            cropY = e.Y;
            //pictureBox2.DoDragDrop(pictureBox2.Image, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                cropHeight = e.Y - cropY;
                cropWidth = e.X - cropX;
                //pictureBox2.DoDragDrop(pictureBox2.Image, DragDropEffects.Copy | DragDropEffects.Move);

                //pictureBox2_Paint();
                //pictureBox2.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);

            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            dragX = dragX + cropWidth;
            dragY = dragY + cropHeight;
            //pictureBox2.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);
            Image img = (Bitmap)Image.FromFile("2.jpg");
            //Bitmap OriginalImage = new Bitmap(img, img.Width, img.Height);
            pictureBox2.Image = null;
            pictureBox2.BackColor = Color.White;
            pictureBox2.DoDragDrop(img, DragDropEffects.Copy | DragDropEffects.Move);
            img.Dispose();
         }
        private void btn_crop1_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image == null)
            {
                MessageBox.Show("Please select image first");
            }

            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            
            //if(File.Exists("1.jpg"))
                Image img = (Bitmap)Image.FromFile("1.jpg");

            Bitmap OriginalImage = new Bitmap(img, img.Width, img.Height);
            Bitmap _img = new Bitmap(cropWidth, cropHeight);
            Graphics g = Graphics.FromImage(_img);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //set image attributes  
            g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);
            pictureBox1.Image = _img;
            img.Dispose();
            new Bitmap(_img, 400, 400).Save("1.jpg");
            //_img.Save("1.jpg");
            
            cropX = cropY = 0;
            cropWidth = cropHeight = 400;
            
        }

        private void btn_crop2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Please select image first");
            }

            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);

            //if(File.Exists("1.jpg"))
            Image img = (Bitmap)Image.FromFile("2.jpg");

            Bitmap OriginalImage = new Bitmap(img, img.Width, img.Height);
            Bitmap _img = new Bitmap(cropWidth, cropHeight);
            Graphics g = Graphics.FromImage(_img);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //set image attributes  
            g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);
            pictureBox2.Image = _img;
            img.Dispose();
            new Bitmap(_img, 400, 400).Save("2.jpg");
            //_img.Save("1.jpg");

            cropX = cropY = 0;
            cropWidth = cropHeight = 400;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            MessageBox.Show(coordinates.X.ToString());
        }
    }
}
