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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.axCoPDFXCview1 = new AxPDFXCviewAxLib.AxCoPDFXCview();
            this.checkButton1 = new DevExpress.XtraEditors.CheckButton();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCoPDFXCview1)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(3, 12);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(307, 220);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // axCoPDFXCview1
            // 
            this.axCoPDFXCview1.Enabled = true;
            this.axCoPDFXCview1.Location = new System.Drawing.Point(539, 2);
            this.axCoPDFXCview1.Name = "axCoPDFXCview1";
            this.axCoPDFXCview1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCoPDFXCview1.OcxState")));
            this.axCoPDFXCview1.Size = new System.Drawing.Size(784, 637);
            this.axCoPDFXCview1.TabIndex = 2;
            // 
            // checkButton1
            // 
            this.checkButton1.Location = new System.Drawing.Point(325, 26);
            this.checkButton1.Name = "checkButton1";
            this.checkButton1.Size = new System.Drawing.Size(110, 67);
            this.checkButton1.TabIndex = 3;
            this.checkButton1.Text = "checkButton1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 694);
            this.Controls.Add(this.checkButton1);
            this.Controls.Add(this.axCoPDFXCview1);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCoPDFXCview1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private AxPDFXCviewAxLib.AxCoPDFXCview axCoPDFXCview1;
        private DevExpress.XtraEditors.CheckButton checkButton1;
        private System.ServiceProcess.ServiceController serviceController1;

    }
}

