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
            this.imagePanel1 = new RichImage.ImagePanel();
            this.SuspendLayout();
            // 
            // imagePanel1
            // 
            this.imagePanel1.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.imagePanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imagePanel1.CausesValidation = false;
            this.imagePanel1.DrawArea_BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.imagePanel1.DrawArea_CompressionX = 1D;
            this.imagePanel1.DrawArea_CompressionY = 1D;
            this.imagePanel1.DrawArea_ImageMonitorPixelSize = new System.Drawing.SizeF(1F, 1F);
            this.imagePanel1.DrawArea_ImageMoveEnabledHeight = true;
            this.imagePanel1.DrawArea_ImageMoveEnabledWidth = true;
            this.imagePanel1.DrawArea_ImagePixelSize = new System.Drawing.SizeF(2F, 2F);
            this.imagePanel1.DrawArea_ImageTempEnabled = false;
            this.imagePanel1.DrawArea_RotateCount = 0;
            this.imagePanel1.DrawArea_ScrollsInvert = false;
            this.imagePanel1.DrawArea_ScrollsType = RichImage.ImagePanelControl.Enums.EnumScrollsType.None;
            this.imagePanel1.DrawArea_StartVisiblePoint = new System.Drawing.Point(0, 0);
            this.imagePanel1.DrawArea_StartVisiblePointX = 0D;
            this.imagePanel1.DrawArea_StartVisiblePointY = 0D;
            this.imagePanel1.DrawArea_Sticking = RichImage.ImagePanelControl.Enums.EnumSticking.None;
            this.imagePanel1.DrawArea_ZoomActualImage = true;
            this.imagePanel1.DrawArea_ZoomBySelectRectangleEnabled = true;
            this.imagePanel1.DrawArea_ZoomInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.imagePanel1.DrawArea_ZoomIsSmoothly = false;
            this.imagePanel1.DrawArea_ZoomMax = 10000;
            this.imagePanel1.DrawArea_ZoomMin = 1;
            this.imagePanel1.DrawArea_ZoomStep = 1;
            this.imagePanel1.DrawArea_ZoomValueFloat = 1F;
            this.imagePanel1.DrawArea_ZoomValuePercent = 100;
            this.imagePanel1.Location = new System.Drawing.Point(124, 112);
            this.imagePanel1.ManagerImageType = RichImage.ImagePanelControl.Enums.EnumManagerImageType.GDIFast;
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.Selected = false;
            this.imagePanel1.SelectedIndicatorEnabled = true;
            this.imagePanel1.Shape_ToolSelect = RichImage.ImagePanelControl.Enums.EnumToolType.Hand;
            this.imagePanel1.Shape_VisibleShapesArea = true;
            this.imagePanel1.Size = new System.Drawing.Size(300, 300);
            this.imagePanel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 512);
            this.Controls.Add(this.imagePanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private RichImage.ImagePanel imagePanel1;

    }
}

