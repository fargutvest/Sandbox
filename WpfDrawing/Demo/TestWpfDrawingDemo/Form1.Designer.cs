namespace TestWpfDrawingDemo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslCurrentScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.wpfUserControl1 = new TestWpfDrawingDemo.WpfUserControl.WpfUserControl();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFant = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHighQuality = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLinear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLowQuality = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNearestNeighbor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnspecified = new System.Windows.Forms.ToolStripMenuItem();
            this.normalScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.elementHost1);
            this.panel1.Location = new System.Drawing.Point(3, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 456);
            this.panel1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCurrentScale});
            this.statusStrip1.Location = new System.Drawing.Point(0, 434);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(555, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslCurrentScale
            // 
            this.tsslCurrentScale.BackColor = System.Drawing.SystemColors.Control;
            this.tsslCurrentScale.Name = "tsslCurrentScale";
            this.tsslCurrentScale.Size = new System.Drawing.Size(74, 17);
            this.tsslCurrentScale.Text = "CurrentScale";
            // 
            // elementHost1
            // 
            this.elementHost1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.elementHost1.BackColorTransparent = true;
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(555, 456);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.wpfUserControl1;
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 150);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.normalScaleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(310, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpen,
            this.tsmiGenerate,
            this.tsmiSave});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(152, 22);
            this.tsmiOpen.Text = "Open";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiGenerate
            // 
            this.tsmiGenerate.Name = "tsmiGenerate";
            this.tsmiGenerate.Size = new System.Drawing.Size(152, 22);
            this.tsmiGenerate.Text = "Generate";
            this.tsmiGenerate.Click += new System.EventHandler(this.tsmiGenerate_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFant,
            this.tsmiHighQuality,
            this.tsmiLinear,
            this.tsmiLowQuality,
            this.tsmiNearestNeighbor,
            this.tsmiUnspecified});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(87, 20);
            this.toolStripMenuItem2.Text = "Interpolation";
            // 
            // tsmiFant
            // 
            this.tsmiFant.Name = "tsmiFant";
            this.tsmiFant.Size = new System.Drawing.Size(164, 22);
            this.tsmiFant.Text = "Fant";
            this.tsmiFant.Click += new System.EventHandler(this.tsmiFant_Click);
            // 
            // tsmiHighQuality
            // 
            this.tsmiHighQuality.Name = "tsmiHighQuality";
            this.tsmiHighQuality.Size = new System.Drawing.Size(164, 22);
            this.tsmiHighQuality.Text = "HighQuality";
            this.tsmiHighQuality.Click += new System.EventHandler(this.tsmiHighQuality_Click);
            // 
            // tsmiLinear
            // 
            this.tsmiLinear.Name = "tsmiLinear";
            this.tsmiLinear.Size = new System.Drawing.Size(164, 22);
            this.tsmiLinear.Text = "Linear";
            this.tsmiLinear.Click += new System.EventHandler(this.tsmiLinear_Click);
            // 
            // tsmiLowQuality
            // 
            this.tsmiLowQuality.Name = "tsmiLowQuality";
            this.tsmiLowQuality.Size = new System.Drawing.Size(164, 22);
            this.tsmiLowQuality.Text = "LowQuality";
            this.tsmiLowQuality.Click += new System.EventHandler(this.tsmiLowQuality_Click);
            // 
            // tsmiNearestNeighbor
            // 
            this.tsmiNearestNeighbor.Name = "tsmiNearestNeighbor";
            this.tsmiNearestNeighbor.Size = new System.Drawing.Size(164, 22);
            this.tsmiNearestNeighbor.Text = "NearestNeighbor";
            this.tsmiNearestNeighbor.Click += new System.EventHandler(this.tsmiNearestNeighbor_Click);
            // 
            // tsmiUnspecified
            // 
            this.tsmiUnspecified.Name = "tsmiUnspecified";
            this.tsmiUnspecified.Size = new System.Drawing.Size(164, 22);
            this.tsmiUnspecified.Text = "Unspecified";
            this.tsmiUnspecified.Click += new System.EventHandler(this.tsmiUnspecified_Click);
            // 
            // normalScaleToolStripMenuItem
            // 
            this.normalScaleToolStripMenuItem.Name = "normalScaleToolStripMenuItem";
            this.normalScaleToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.normalScaleToolStripMenuItem.Text = "NormalScale";
            this.normalScaleToolStripMenuItem.Click += new System.EventHandler(this.normalScaleToolStripMenuItem_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(152, 22);
            this.tsmiSave.Text = "Save";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 504);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private WpfUserControl.WpfUserControl wpfUserControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiFant;
        private System.Windows.Forms.ToolStripMenuItem tsmiHighQuality;
        private System.Windows.Forms.ToolStripMenuItem tsmiLinear;
        private System.Windows.Forms.ToolStripMenuItem tsmiLowQuality;
        private System.Windows.Forms.ToolStripMenuItem tsmiNearestNeighbor;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnspecified;
        private System.Windows.Forms.ToolStripMenuItem normalScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiGenerate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrentScale;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;

    }
}

