
namespace Cricket_Game_client_
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
            this.btnplay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnplay)).BeginInit();
            this.SuspendLayout();
            // 
            // btnplay
            // 
            this.btnplay.BackColor = System.Drawing.Color.Transparent;
            this.btnplay.Image = global::Cricket_Game_client_.Properties.Resources.btnplay;
            this.btnplay.Location = new System.Drawing.Point(629, 473);
            this.btnplay.Name = "btnplay";
            this.btnplay.Size = new System.Drawing.Size(157, 68);
            this.btnplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnplay.TabIndex = 4;
            this.btnplay.TabStop = false;
            this.btnplay.Click += new System.EventHandler(this.btnplay_Click);
            this.btnplay.MouseEnter += new System.EventHandler(this.btnplay_MouseEnter);
            this.btnplay.MouseLeave += new System.EventHandler(this.btnplay_MouseLeave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Cricket_Game_client_.Properties.Resources.zkUBfweq_4x;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(798, 564);
            this.Controls.Add(this.btnplay);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.btnplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox btnplay;
    }
}

