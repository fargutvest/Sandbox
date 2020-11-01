namespace MusicDownloaderVK
{
    partial class MyForm
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
            this.lvMusicList = new System.Windows.Forms.ListView();
            this.lbMusicList = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnNewDir = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tbPageId = new System.Windows.Forms.TextBox();
            this.rbUser = new System.Windows.Forms.RadioButton();
            this.rbGroup = new System.Windows.Forms.RadioButton();
            this.lbId = new System.Windows.Forms.Label();
            this.btnAuthorise = new System.Windows.Forms.Button();
            this.tbProgressCurrent = new System.Windows.Forms.TextBox();
            this.pbCurrent = new System.Windows.Forms.ProgressBar();
            this.pbAll = new System.Windows.Forms.ProgressBar();
            this.btStopDownload = new System.Windows.Forms.Button();
            this.tbPathFolder = new System.Windows.Forms.TextBox();
            this.lbPhone = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lvMusicList
            // 
            this.lvMusicList.Location = new System.Drawing.Point(1, 121);
            this.lvMusicList.Name = "lvMusicList";
            this.lvMusicList.Size = new System.Drawing.Size(705, 298);
            this.lvMusicList.TabIndex = 0;
            this.lvMusicList.UseCompatibleStateImageBehavior = false;
            this.lvMusicList.View = System.Windows.Forms.View.Tile;
            // 
            // lbMusicList
            // 
            this.lbMusicList.AutoSize = true;
            this.lbMusicList.Location = new System.Drawing.Point(12, 102);
            this.lbMusicList.Name = "lbMusicList";
            this.lbMusicList.Size = new System.Drawing.Size(54, 13);
            this.lbMusicList.TabIndex = 3;
            this.lbMusicList.Text = "Music List";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(331, 423);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.Download_Click);
            // 
            // btnNewDir
            // 
            this.btnNewDir.Location = new System.Drawing.Point(593, 495);
            this.btnNewDir.Name = "btnNewDir";
            this.btnNewDir.Size = new System.Drawing.Size(113, 20);
            this.btnNewDir.TabIndex = 6;
            this.btnNewDir.Text = "Change path folder";
            this.btnNewDir.UseVisualStyleBackColor = true;
            this.btnNewDir.Click += new System.EventHandler(this.OnNewDir_Click);
            // 
            // tbPageId
            // 
            this.tbPageId.Location = new System.Drawing.Point(55, 425);
            this.tbPageId.Name = "tbPageId";
            this.tbPageId.Size = new System.Drawing.Size(150, 20);
            this.tbPageId.TabIndex = 7;
            // 
            // rbUser
            // 
            this.rbUser.AutoSize = true;
            this.rbUser.Checked = true;
            this.rbUser.Location = new System.Drawing.Point(211, 425);
            this.rbUser.Name = "rbUser";
            this.rbUser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rbUser.Size = new System.Drawing.Size(47, 17);
            this.rbUser.TabIndex = 8;
            this.rbUser.TabStop = true;
            this.rbUser.Text = "User";
            this.rbUser.UseVisualStyleBackColor = true;
            // 
            // rbGroup
            // 
            this.rbGroup.AutoSize = true;
            this.rbGroup.Location = new System.Drawing.Point(262, 425);
            this.rbGroup.Name = "rbGroup";
            this.rbGroup.Size = new System.Drawing.Size(54, 17);
            this.rbGroup.TabIndex = 9;
            this.rbGroup.TabStop = true;
            this.rbGroup.Text = "Group";
            this.rbGroup.UseVisualStyleBackColor = true;
            // 
            // lbId
            // 
            this.lbId.AutoSize = true;
            this.lbId.Location = new System.Drawing.Point(3, 429);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(46, 13);
            this.lbId.TabIndex = 10;
            this.lbId.Text = "Page id:";
            // 
            // btnAuthorise
            // 
            this.btnAuthorise.Location = new System.Drawing.Point(221, 12);
            this.btnAuthorise.Name = "btnAuthorise";
            this.btnAuthorise.Size = new System.Drawing.Size(75, 47);
            this.btnAuthorise.TabIndex = 11;
            this.btnAuthorise.Text = "Authorise";
            this.btnAuthorise.UseVisualStyleBackColor = true;
            this.btnAuthorise.Click += new System.EventHandler(this.Authorise_Click);
            // 
            // tbProgressCurrent
            // 
            this.tbProgressCurrent.BackColor = System.Drawing.SystemColors.Menu;
            this.tbProgressCurrent.Location = new System.Drawing.Point(1, 452);
            this.tbProgressCurrent.Name = "tbProgressCurrent";
            this.tbProgressCurrent.ReadOnly = true;
            this.tbProgressCurrent.Size = new System.Drawing.Size(705, 20);
            this.tbProgressCurrent.TabIndex = 12;
            // 
            // pbCurrent
            // 
            this.pbCurrent.Location = new System.Drawing.Point(1, 473);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(705, 8);
            this.pbCurrent.Step = 1;
            this.pbCurrent.TabIndex = 13;
            // 
            // pbAll
            // 
            this.pbAll.Location = new System.Drawing.Point(1, 481);
            this.pbAll.Name = "pbAll";
            this.pbAll.Size = new System.Drawing.Size(705, 8);
            this.pbAll.Step = 1;
            this.pbAll.TabIndex = 14;
            // 
            // btStopDownload
            // 
            this.btStopDownload.Location = new System.Drawing.Point(412, 422);
            this.btStopDownload.Name = "btStopDownload";
            this.btStopDownload.Size = new System.Drawing.Size(75, 23);
            this.btStopDownload.TabIndex = 15;
            this.btStopDownload.Text = "Stop";
            this.btStopDownload.UseVisualStyleBackColor = true;
            this.btStopDownload.Click += new System.EventHandler(this.OnStopDownload_Click);
            // 
            // tbPathFolder
            // 
            this.tbPathFolder.Location = new System.Drawing.Point(1, 495);
            this.tbPathFolder.Name = "tbPathFolder";
            this.tbPathFolder.ReadOnly = true;
            this.tbPathFolder.Size = new System.Drawing.Size(586, 20);
            this.tbPathFolder.TabIndex = 16;
            // 
            // lbPhone
            // 
            this.lbPhone.AutoSize = true;
            this.lbPhone.Location = new System.Drawing.Point(3, 16);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Size = new System.Drawing.Size(41, 13);
            this.lbPhone.TabIndex = 18;
            this.lbPhone.Text = "Phone:";
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(65, 13);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(150, 20);
            this.tbPhone.TabIndex = 17;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(3, 42);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(56, 13);
            this.lbPassword.TabIndex = 20;
            this.lbPassword.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(65, 39);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(150, 20);
            this.tbPassword.TabIndex = 19;
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 521);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lbPhone);
            this.Controls.Add(this.tbPhone);
            this.Controls.Add(this.tbPathFolder);
            this.Controls.Add(this.btStopDownload);
            this.Controls.Add(this.pbAll);
            this.Controls.Add(this.pbCurrent);
            this.Controls.Add(this.tbProgressCurrent);
            this.Controls.Add(this.btnAuthorise);
            this.Controls.Add(this.lbId);
            this.Controls.Add(this.rbGroup);
            this.Controls.Add(this.rbUser);
            this.Controls.Add(this.tbPageId);
            this.Controls.Add(this.btnNewDir);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lbMusicList);
            this.Controls.Add(this.lvMusicList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MyForm";
            this.Text = "Music Downloader VK";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMusicList;
        private System.Windows.Forms.Label lbMusicList;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnNewDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tbPageId;
        private System.Windows.Forms.RadioButton rbUser;
        private System.Windows.Forms.RadioButton rbGroup;
        private System.Windows.Forms.Label lbId;
        private System.Windows.Forms.Button btnAuthorise;
        private System.Windows.Forms.TextBox tbProgressCurrent;
        private System.Windows.Forms.ProgressBar pbCurrent;
        private System.Windows.Forms.ProgressBar pbAll;
        private System.Windows.Forms.Button btStopDownload;
        private System.Windows.Forms.TextBox tbPathFolder;
        private System.Windows.Forms.Label lbPhone;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox tbPassword;
    }
}

