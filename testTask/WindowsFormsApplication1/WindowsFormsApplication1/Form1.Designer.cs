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
            this.btStopTask = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btStopTask
            // 
            this.btStopTask.Location = new System.Drawing.Point(28, 149);
            this.btStopTask.Name = "btStopTask";
            this.btStopTask.Size = new System.Drawing.Size(75, 23);
            this.btStopTask.TabIndex = 0;
            this.btStopTask.Text = "StopTask";
            this.btStopTask.UseVisualStyleBackColor = true;
            this.btStopTask.Click += new System.EventHandler(this.btStopTask_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btStopTask);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btStopTask;
    }
}

