namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnToArrayByte = new System.Windows.Forms.Button();
            this.btnToBMP = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(80, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "open bmp";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnToArrayByte
            // 
            this.btnToArrayByte.Location = new System.Drawing.Point(12, 70);
            this.btnToArrayByte.Name = "btnToArrayByte";
            this.btnToArrayByte.Size = new System.Drawing.Size(80, 23);
            this.btnToArrayByte.TabIndex = 1;
            this.btnToArrayByte.Text = "bmp -> byte[]";
            this.btnToArrayByte.UseVisualStyleBackColor = true;
            this.btnToArrayByte.Click += new System.EventHandler(this.btnToArrayByte_Click);
            // 
            // btnToBMP
            // 
            this.btnToBMP.Location = new System.Drawing.Point(12, 41);
            this.btnToBMP.Name = "btnToBMP";
            this.btnToBMP.Size = new System.Drawing.Size(80, 23);
            this.btnToBMP.TabIndex = 2;
            this.btnToBMP.Text = "byte[] -> bmp";
            this.btnToBMP.UseVisualStyleBackColor = true;
            this.btnToBMP.Click += new System.EventHandler(this.btnToBMP_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(98, 17);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(34, 13);
            this.lblPath.TabIndex = 3;
            this.lblPath.Text = "patch";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 262);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnToBMP);
            this.Controls.Add(this.btnToArrayByte);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.Text = "convert bmp to byte[] to bmp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnToArrayByte;
        private System.Windows.Forms.Button btnToBMP;
        private System.Windows.Forms.Label lblPath;
    }
}

