namespace WindowsFormsApplication1
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
            this.rchtbOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rchtbOutput
            // 
            this.rchtbOutput.Location = new System.Drawing.Point(4, 3);
            this.rchtbOutput.Name = "rchtbOutput";
            this.rchtbOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rchtbOutput.Size = new System.Drawing.Size(278, 256);
            this.rchtbOutput.TabIndex = 0;
            this.rchtbOutput.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.rchtbOutput);
            this.Name = "Form1";
            this.Text = "bbb";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rchtbOutput;
    }
}

