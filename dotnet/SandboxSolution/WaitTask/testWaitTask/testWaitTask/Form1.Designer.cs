namespace testWaitTask
{
    partial class Form1
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
            this.btStartTask = new System.Windows.Forms.Button();
            this.btDisposeTask = new System.Windows.Forms.Button();
            this.btAreSet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btStartTask
            // 
            this.btStartTask.Location = new System.Drawing.Point(30, 61);
            this.btStartTask.Name = "btStartTask";
            this.btStartTask.Size = new System.Drawing.Size(75, 23);
            this.btStartTask.TabIndex = 0;
            this.btStartTask.Text = "start task";
            this.btStartTask.UseVisualStyleBackColor = true;
            this.btStartTask.Click += new System.EventHandler(this.btStartTask_Click);
            // 
            // btDisposeTask
            // 
            this.btDisposeTask.Location = new System.Drawing.Point(140, 61);
            this.btDisposeTask.Name = "btDisposeTask";
            this.btDisposeTask.Size = new System.Drawing.Size(75, 23);
            this.btDisposeTask.TabIndex = 1;
            this.btDisposeTask.Text = "dispose task";
            this.btDisposeTask.UseVisualStyleBackColor = true;
            this.btDisposeTask.Click += new System.EventHandler(this.btDisposeTask_Click);
            // 
            // btAreSet
            // 
            this.btAreSet.Location = new System.Drawing.Point(105, 120);
            this.btAreSet.Name = "btAreSet";
            this.btAreSet.Size = new System.Drawing.Size(75, 23);
            this.btAreSet.TabIndex = 2;
            this.btAreSet.Text = "are set";
            this.btAreSet.UseVisualStyleBackColor = true;
            this.btAreSet.Click += new System.EventHandler(this.btAreSet_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btAreSet);
            this.Controls.Add(this.btDisposeTask);
            this.Controls.Add(this.btStartTask);
            this.Name = "Form1";
            this.Text = "test wait task";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btStartTask;
        private System.Windows.Forms.Button btDisposeTask;
        private System.Windows.Forms.Button btAreSet;
    }
}

