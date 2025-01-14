
namespace CaptureImage.WinForms
{
    partial class BlackoutScreen
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
            this.SuspendLayout();
            // 
            // BlackoutScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Name = "BlackoutScreen";
            this.Text = "BlackoutScreen";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BlackoutScreen_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlackoutScreen_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlackoutScreen_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BlackoutScreen_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}