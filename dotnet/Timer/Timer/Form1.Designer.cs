namespace Timer
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
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.lbOutputTime = new System.Windows.Forms.Label();
            this.lbOutputDays = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.BackColor = System.Drawing.Color.Orange;
            this.btStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btStart.Location = new System.Drawing.Point(30, 302);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(80, 50);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = false;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.BackColor = System.Drawing.Color.Orange;
            this.btStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btStop.Location = new System.Drawing.Point(202, 302);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(80, 50);
            this.btStop.TabIndex = 1;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = false;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // btReset
            // 
            this.btReset.BackColor = System.Drawing.Color.Orange;
            this.btReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btReset.Location = new System.Drawing.Point(116, 302);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(80, 50);
            this.btReset.TabIndex = 2;
            this.btReset.Text = "Reset";
            this.btReset.UseVisualStyleBackColor = false;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // lbOutputTime
            // 
            this.lbOutputTime.AutoSize = true;
            this.lbOutputTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbOutputTime.ForeColor = System.Drawing.Color.Orange;
            this.lbOutputTime.Location = new System.Drawing.Point(12, 138);
            this.lbOutputTime.Name = "lbOutputTime";
            this.lbOutputTime.Size = new System.Drawing.Size(589, 108);
            this.lbOutputTime.TabIndex = 3;
            this.lbOutputTime.Text = "HH::MM::SS";
            // 
            // lbOutputDays
            // 
            this.lbOutputDays.AutoSize = true;
            this.lbOutputDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbOutputDays.ForeColor = System.Drawing.Color.Orange;
            this.lbOutputDays.Location = new System.Drawing.Point(12, 9);
            this.lbOutputDays.Name = "lbOutputDays";
            this.lbOutputDays.Size = new System.Drawing.Size(322, 108);
            this.lbOutputDays.TabIndex = 4;
            this.lbOutputDays.Text = "Days: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 264);
            this.Controls.Add(this.lbOutputDays);
            this.Controls.Add(this.lbOutputTime);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Timer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Label lbOutputTime;
        private System.Windows.Forms.Label lbOutputDays;
    }
}

