
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
            this.MouseMove += BlackoutScreen_MouseMove;
            this.MouseDown += BlackoutScreen_MouseDown;
            this.MouseUp += BlackoutScreen_MouseUp;
            this.ResumeLayout(false);
        }

        #endregion
    }
}