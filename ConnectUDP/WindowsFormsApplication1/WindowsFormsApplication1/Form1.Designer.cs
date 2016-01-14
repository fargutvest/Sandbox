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
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rchtbInput = new System.Windows.Forms.RichTextBox();
            this.rbSender = new System.Windows.Forms.RadioButton();
            this.rbReceiver = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(45, 12);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(75, 20);
            this.tbPort.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "port:";
            // 
            // rchtbInput
            // 
            this.rchtbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rchtbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rchtbInput.Location = new System.Drawing.Point(12, 50);
            this.rchtbInput.Name = "rchtbInput";
            this.rchtbInput.Size = new System.Drawing.Size(260, 211);
            this.rchtbInput.TabIndex = 3;
            this.rchtbInput.Text = "";
            this.rchtbInput.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // rbSender
            // 
            this.rbSender.AutoSize = true;
            this.rbSender.Location = new System.Drawing.Point(146, 11);
            this.rbSender.Name = "rbSender";
            this.rbSender.Size = new System.Drawing.Size(57, 17);
            this.rbSender.TabIndex = 4;
            this.rbSender.TabStop = true;
            this.rbSender.Text = "sender";
            this.rbSender.UseVisualStyleBackColor = true;
            this.rbSender.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbReceiver
            // 
            this.rbReceiver.AutoSize = true;
            this.rbReceiver.Location = new System.Drawing.Point(146, 27);
            this.rbReceiver.Name = "rbReceiver";
            this.rbReceiver.Size = new System.Drawing.Size(63, 17);
            this.rbReceiver.TabIndex = 5;
            this.rbReceiver.TabStop = true;
            this.rbReceiver.Text = "receiver";
            this.rbReceiver.UseVisualStyleBackColor = true;
            this.rbReceiver.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.rbReceiver);
            this.Controls.Add(this.rbSender);
            this.Controls.Add(this.rchtbInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPort);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rchtbInput;
        private System.Windows.Forms.RadioButton rbSender;
        private System.Windows.Forms.RadioButton rbReceiver;

    }
}

