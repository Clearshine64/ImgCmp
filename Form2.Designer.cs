
namespace ImgCmp
{
    partial class Form2
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
            this.RTB1 = new System.Windows.Forms.RichTextBox();
            this.RTB2 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RTB1
            // 
            this.RTB1.Location = new System.Drawing.Point(12, 32);
            this.RTB1.Name = "RTB1";
            this.RTB1.Size = new System.Drawing.Size(542, 740);
            this.RTB1.TabIndex = 0;
            this.RTB1.Text = "";
            this.RTB1.VScroll += new System.EventHandler(this.RTB1_VScroll);
            // 
            // RTB2
            // 
            this.RTB2.Location = new System.Drawing.Point(560, 32);
            this.RTB2.Name = "RTB2";
            this.RTB2.Size = new System.Drawing.Size(542, 740);
            this.RTB2.TabIndex = 1;
            this.RTB2.Text = "";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 784);
            this.Controls.Add(this.RTB2);
            this.Controls.Add(this.RTB1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RTB1;
        private System.Windows.Forms.RichTextBox RTB2;
    }
}