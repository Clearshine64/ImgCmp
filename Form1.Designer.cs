
namespace ImgCmp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_firstImg = new System.Windows.Forms.LinkLabel();
            this.label_secondImg = new System.Windows.Forms.LinkLabel();
            this.btn_picture_compare = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.btn_text_compare = new System.Windows.Forms.Button();
            this.btn_table_compare = new System.Windows.Forms.Button();
            this.btn_hex_compare = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_image_gap = new System.Windows.Forms.Button();
            this.btn_image_overlap = new System.Windows.Forms.Button();
            this.btn_crop1 = new System.Windows.Forms.Button();
            this.btn_crop2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label_firstImg
            // 
            this.label_firstImg.AutoSize = true;
            this.label_firstImg.Location = new System.Drawing.Point(26, 27);
            this.label_firstImg.Name = "label_firstImg";
            this.label_firstImg.Size = new System.Drawing.Size(91, 13);
            this.label_firstImg.TabIndex = 0;
            this.label_firstImg.TabStop = true;
            this.label_firstImg.Text = "選擇第一張圖片";
            this.label_firstImg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_firstImg_LinkClicked);
            // 
            // label_secondImg
            // 
            this.label_secondImg.AutoSize = true;
            this.label_secondImg.Location = new System.Drawing.Point(26, 58);
            this.label_secondImg.Name = "label_secondImg";
            this.label_secondImg.Size = new System.Drawing.Size(91, 13);
            this.label_secondImg.TabIndex = 1;
            this.label_secondImg.TabStop = true;
            this.label_secondImg.Text = "選擇第二張圖片";
            this.label_secondImg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_secondImg_LinkClicked);
            // 
            // btn_picture_compare
            // 
            this.btn_picture_compare.Location = new System.Drawing.Point(29, 112);
            this.btn_picture_compare.Name = "btn_picture_compare";
            this.btn_picture_compare.Size = new System.Drawing.Size(115, 23);
            this.btn_picture_compare.TabIndex = 2;
            this.btn_picture_compare.Text = "圖片比較";
            this.btn_picture_compare.UseVisualStyleBackColor = true;
            this.btn_picture_compare.Click += new System.EventHandler(this.btn_picture_compare_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(178, 110);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            //this.pictureBox1.Paint += pictureBox1_Paint;
            this.pictureBox1.MouseDown += pictureBox1_MouseDown;
            this.pictureBox1.MouseMove += pictureBox1_MouseMove;
            this.pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(591, 110);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 400);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += pictureBox2_MouseDown;
            this.pictureBox2.MouseMove += pictureBox2_MouseMove;
            this.pictureBox2.MouseUp += pictureBox2_MouseUp;
            this.pictureBox2.DragDrop += pictureBox2_DragDrop;
            this.pictureBox2.DragEnter += pictureBox2_DragEnter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(1005, 110);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(400, 400);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog1";
            // 
            // btn_text_compare
            // 
            this.btn_text_compare.Location = new System.Drawing.Point(29, 152);
            this.btn_text_compare.Name = "btn_text_compare";
            this.btn_text_compare.Size = new System.Drawing.Size(115, 23);
            this.btn_text_compare.TabIndex = 7;
            this.btn_text_compare.Text = "文字比較";
            this.btn_text_compare.UseVisualStyleBackColor = true;
            this.btn_text_compare.Click += new System.EventHandler(this.btn_text_compare_Click);
            // 
            // btn_table_compare
            // 
            this.btn_table_compare.Location = new System.Drawing.Point(29, 192);
            this.btn_table_compare.Name = "btn_table_compare";
            this.btn_table_compare.Size = new System.Drawing.Size(115, 23);
            this.btn_table_compare.TabIndex = 8;
            this.btn_table_compare.Text = "元數據比較";
            this.btn_table_compare.UseVisualStyleBackColor = true;
            this.btn_table_compare.Click += new System.EventHandler(this.btn_table_compare_Click);
            // 
            // btn_hex_compare
            // 
            this.btn_hex_compare.Location = new System.Drawing.Point(29, 232);
            this.btn_hex_compare.Name = "btn_hex_compare";
            this.btn_hex_compare.Size = new System.Drawing.Size(115, 23);
            this.btn_hex_compare.TabIndex = 9;
            this.btn_hex_compare.Text = "Hex 相比";
            this.btn_hex_compare.UseVisualStyleBackColor = true;
            this.btn_hex_compare.Click += new System.EventHandler(this.btn_hex_compare_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1350, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "比較結果";
            // 
            // btn_image_gap
            // 
            this.btn_image_gap.Location = new System.Drawing.Point(29, 273);
            this.btn_image_gap.Name = "btn_image_gap";
            this.btn_image_gap.Size = new System.Drawing.Size(115, 23);
            this.btn_image_gap.TabIndex = 13;
            this.btn_image_gap.Text = "圖像間隙";
            this.btn_image_gap.UseVisualStyleBackColor = true;
            this.btn_image_gap.Click += new System.EventHandler(this.btn_image_gap_Click);
            // 
            // btn_image_overlap
            // 
            this.btn_image_overlap.Location = new System.Drawing.Point(29, 312);
            this.btn_image_overlap.Name = "btn_image_overlap";
            this.btn_image_overlap.Size = new System.Drawing.Size(115, 23);
            this.btn_image_overlap.TabIndex = 14;
            this.btn_image_overlap.Text = "圖像疊加";
            this.btn_image_overlap.UseVisualStyleBackColor = true;
            this.btn_image_overlap.Click += new System.EventHandler(this.btn_image_overlap_Click);
            // 
            // btn_crop1
            // 
            this.btn_crop1.Location = new System.Drawing.Point(509, 82);
            this.btn_crop1.Name = "btn_crop1";
            this.btn_crop1.Size = new System.Drawing.Size(69, 23);
            this.btn_crop1.TabIndex = 15;
            this.btn_crop1.Text = "裁剪圖像";
            this.btn_crop1.UseVisualStyleBackColor = true;
            this.btn_crop1.Click += new System.EventHandler(this.btn_crop1_Click);
            // 
            // btn_crop2
            // 
            this.btn_crop2.Location = new System.Drawing.Point(922, 82);
            this.btn_crop2.Name = "btn_crop2";
            this.btn_crop2.Size = new System.Drawing.Size(69, 23);
            this.btn_crop2.TabIndex = 16;
            this.btn_crop2.Text = "裁剪圖像";
            this.btn_crop2.UseVisualStyleBackColor = true;
            this.btn_crop2.Click += new System.EventHandler(this.btn_crop2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1462, 555);
            this.Controls.Add(this.btn_crop2);
            this.Controls.Add(this.btn_crop1);
            this.Controls.Add(this.btn_image_overlap);
            this.Controls.Add(this.btn_image_gap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_hex_compare);
            this.Controls.Add(this.btn_table_compare);
            this.Controls.Add(this.btn_text_compare);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_picture_compare);
            this.Controls.Add(this.label_secondImg);
            this.Controls.Add(this.label_firstImg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel label_firstImg;
        private System.Windows.Forms.LinkLabel label_secondImg;
        private System.Windows.Forms.Button btn_picture_compare;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button btn_text_compare;
        private System.Windows.Forms.Button btn_table_compare;
        private System.Windows.Forms.Button btn_hex_compare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_image_gap;
        private System.Windows.Forms.Button btn_image_overlap;
        private System.Windows.Forms.Button btn_crop1;
        private System.Windows.Forms.Button btn_crop2;
    }
}

