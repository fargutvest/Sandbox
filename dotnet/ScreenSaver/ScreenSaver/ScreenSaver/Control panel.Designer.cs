namespace ScreenSaver
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
            this.label1 = new System.Windows.Forms.Label();
            this.starSizeTb = new System.Windows.Forms.TrackBar();
            this.starCountTb = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.starColorTb = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.moveStarTb = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.moveStarValue = new System.Windows.Forms.Label();
            this.starSizeValue = new System.Windows.Forms.Label();
            this.starCountValue = new System.Windows.Forms.Label();
            this.starColorValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.starSizeTb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.starCountTb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.starColorTb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveStarTb)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Star size";
            // 
            // starSizeTb
            // 
            this.starSizeTb.Location = new System.Drawing.Point(12, 105);
            this.starSizeTb.Name = "starSizeTb";
            this.starSizeTb.Size = new System.Drawing.Size(360, 45);
            this.starSizeTb.TabIndex = 2;
            this.starSizeTb.Scroll += new System.EventHandler(this.starSizeTb_Scroll);
            // 
            // starCountTb
            // 
            this.starCountTb.Location = new System.Drawing.Point(12, 172);
            this.starCountTb.Maximum = 15000;
            this.starCountTb.Name = "starCountTb";
            this.starCountTb.Size = new System.Drawing.Size(360, 45);
            this.starCountTb.TabIndex = 4;
            this.starCountTb.TickFrequency = 100;
            this.starCountTb.Scroll += new System.EventHandler(this.starCountTb_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Star count";
            // 
            // starColorTb
            // 
            this.starColorTb.Location = new System.Drawing.Point(12, 236);
            this.starColorTb.Name = "starColorTb";
            this.starColorTb.Size = new System.Drawing.Size(360, 45);
            this.starColorTb.TabIndex = 6;
            this.starColorTb.Scroll += new System.EventHandler(this.starColorTb_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Star color";
            // 
            // moveStarTb
            // 
            this.moveStarTb.Location = new System.Drawing.Point(12, 38);
            this.moveStarTb.Maximum = 100;
            this.moveStarTb.Name = "moveStarTb";
            this.moveStarTb.Size = new System.Drawing.Size(360, 45);
            this.moveStarTb.TabIndex = 8;
            this.moveStarTb.Scroll += new System.EventHandler(this.moveStarTb_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Move star";
            // 
            // moveStarValue
            // 
            this.moveStarValue.AutoSize = true;
            this.moveStarValue.Location = new System.Drawing.Point(72, 22);
            this.moveStarValue.Name = "moveStarValue";
            this.moveStarValue.Size = new System.Drawing.Size(54, 13);
            this.moveStarValue.TabIndex = 9;
            this.moveStarValue.Text = "Move star";
            // 
            // starSizeValue
            // 
            this.starSizeValue.AutoSize = true;
            this.starSizeValue.Location = new System.Drawing.Point(72, 86);
            this.starSizeValue.Name = "starSizeValue";
            this.starSizeValue.Size = new System.Drawing.Size(47, 13);
            this.starSizeValue.TabIndex = 10;
            this.starSizeValue.Text = "Star size";
            // 
            // starCountValue
            // 
            this.starCountValue.AutoSize = true;
            this.starCountValue.Location = new System.Drawing.Point(74, 153);
            this.starCountValue.Name = "starCountValue";
            this.starCountValue.Size = new System.Drawing.Size(56, 13);
            this.starCountValue.TabIndex = 11;
            this.starCountValue.Text = "Star count";
            // 
            // starColorValue
            // 
            this.starColorValue.AutoSize = true;
            this.starColorValue.Location = new System.Drawing.Point(74, 220);
            this.starColorValue.Name = "starColorValue";
            this.starColorValue.Size = new System.Drawing.Size(52, 13);
            this.starColorValue.TabIndex = 12;
            this.starColorValue.Text = "Star color";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.starColorValue);
            this.Controls.Add(this.starCountValue);
            this.Controls.Add(this.starSizeValue);
            this.Controls.Add(this.moveStarValue);
            this.Controls.Add(this.moveStarTb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.starColorTb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.starCountTb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.starSizeTb);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form2";
            this.Text = "Control panel";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.starSizeTb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.starCountTb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.starColorTb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveStarTb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar starSizeTb;
        private System.Windows.Forms.TrackBar starCountTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar starColorTb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar moveStarTb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label moveStarValue;
        private System.Windows.Forms.Label starSizeValue;
        private System.Windows.Forms.Label starCountValue;
        private System.Windows.Forms.Label starColorValue;
    }
}