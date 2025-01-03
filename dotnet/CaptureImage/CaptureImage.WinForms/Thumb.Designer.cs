﻿using System.Drawing;

namespace CaptureImage.WinForms
{
    partial class Thumb
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Thumb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.DoubleBuffered = true;
            this.Name = "Thumb";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Thumb_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Thumb_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Thumb_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Thumb_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Thumb_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Thumb_MouseUp);
            this.ResumeLayout(false);

        }




        #endregion
    }
}