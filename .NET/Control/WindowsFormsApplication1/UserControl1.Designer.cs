namespace WindowsFormsApplication1
{
    partial class MyControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MyButton
            // 
            this.MyButton.Location = new System.Drawing.Point(3, 3);
            this.MyButton.Name = "MyButton";
            this.MyButton.Size = new System.Drawing.Size(75, 23);
            this.MyButton.TabIndex = 0;
            this.MyButton.Text = "button1";
            this.MyButton.UseVisualStyleBackColor = true;
            // 
            // MyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MyButton);
            this.Name = "MyControl";
            this.Size = new System.Drawing.Size(86, 31);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MyButton;
    }
}
