namespace DrawLines
{
    partial class Designer
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
            this.drawSurface = new DrawLines.NoFlickerPanel();
            this.SuspendLayout();
            // 
            // drawSurface
            // 
            this.drawSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawSurface.Location = new System.Drawing.Point(0, 0);
            this.drawSurface.Name = "drawSurface";
            this.drawSurface.Size = new System.Drawing.Size(1006, 977);
            this.drawSurface.TabIndex = 0;
            this.drawSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.drawSurface_Paint);
            this.drawSurface.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawSurface_MouseClick);
            this.drawSurface.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawSurface_MouseMove);
            // 
            // Designer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 977);
            this.Controls.Add(this.drawSurface);
            this.Name = "Designer";
            this.Text = "Designer";
            this.ResumeLayout(false);

        }

        #endregion

        private NoFlickerPanel drawSurface;
    }
}