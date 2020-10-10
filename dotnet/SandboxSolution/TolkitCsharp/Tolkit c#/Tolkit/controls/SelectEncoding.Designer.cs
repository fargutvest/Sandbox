namespace Tolkit.controls
{
    partial class SelectEncoding
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
            this.rbUnicode = new System.Windows.Forms.RadioButton();
            this.rbHex = new System.Windows.Forms.RadioButton();
            this.rbAscii = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbUnicode
            // 
            this.rbUnicode.AutoSize = true;
            this.rbUnicode.Location = new System.Drawing.Point(3, 49);
            this.rbUnicode.Name = "rbUnicode";
            this.rbUnicode.Size = new System.Drawing.Size(65, 17);
            this.rbUnicode.TabIndex = 17;
            this.rbUnicode.Text = "Unicode";
            this.rbUnicode.UseVisualStyleBackColor = true;
            this.rbUnicode.CheckedChanged += new System.EventHandler(this.rbUnicode_CheckedChanged);
            // 
            // rbInHex
            // 
            this.rbHex.AutoSize = true;
            this.rbHex.Checked = true;
            this.rbHex.Location = new System.Drawing.Point(3, 3);
            this.rbHex.Name = "rbInHex";
            this.rbHex.Size = new System.Drawing.Size(44, 17);
            this.rbHex.TabIndex = 15;
            this.rbHex.TabStop = true;
            this.rbHex.Text = "Hex";
            this.rbHex.UseVisualStyleBackColor = true;
            this.rbHex.CheckedChanged += new System.EventHandler(this.rbHex_CheckedChanged);
            // 
            // rbInAscii
            // 
            this.rbAscii.AutoSize = true;
            this.rbAscii.Location = new System.Drawing.Point(3, 26);
            this.rbAscii.Name = "rbInAscii";
            this.rbAscii.Size = new System.Drawing.Size(47, 17);
            this.rbAscii.TabIndex = 16;
            this.rbAscii.Text = "Ascii";
            this.rbAscii.UseVisualStyleBackColor = true;
            this.rbAscii.CheckedChanged += new System.EventHandler(this.rbAscii_CheckedChanged);
            // 
            // SelectEncoding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbUnicode);
            this.Controls.Add(this.rbHex);
            this.Controls.Add(this.rbAscii);
            this.Name = "SelectEncoding";
            this.Size = new System.Drawing.Size(66, 66);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RadioButton rbUnicode;
        public System.Windows.Forms.RadioButton rbHex;
        public System.Windows.Forms.RadioButton rbAscii;

    }
}
