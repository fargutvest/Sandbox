namespace testDetectorCalibration
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
            this.btnSnap = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btGainCalibr = new System.Windows.Forms.Button();
            this.tbPixelGain = new System.Windows.Forms.TextBox();
            this.tbPixelOffset = new System.Windows.Forms.TextBox();
            this.tbValueOffset = new System.Windows.Forms.TextBox();
            this.tbValueGain = new System.Windows.Forms.TextBox();
            this.btOffseCalibr = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rchtbMonCmd = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btReadGainOffset = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rbStatic = new System.Windows.Forms.RadioButton();
            this.rbRealTime = new System.Windows.Forms.RadioButton();
            this.lvPixelInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbCommand = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btSend = new System.Windows.Forms.Button();
            this.btResetGainOffset = new System.Windows.Forms.Button();
            this.tbNormVal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btWriteNewG_O = new System.Windows.Forms.Button();
            this.btCalcG_O = new System.Windows.Forms.Button();
            this.btStartFrame = new System.Windows.Forms.Button();
            this.btStopFrame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSnap
            // 
            this.btnSnap.Location = new System.Drawing.Point(10, 21);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(79, 23);
            this.btnSnap.TabIndex = 0;
            this.btnSnap.Text = "start work";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pictureBox1.Location = new System.Drawing.Point(43, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 255);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btGainCalibr
            // 
            this.btGainCalibr.Location = new System.Drawing.Point(145, 41);
            this.btGainCalibr.Name = "btGainCalibr";
            this.btGainCalibr.Size = new System.Drawing.Size(55, 23);
            this.btGainCalibr.TabIndex = 5;
            this.btGainCalibr.Text = "gain";
            this.btGainCalibr.UseVisualStyleBackColor = true;
            this.btGainCalibr.Click += new System.EventHandler(this.btGainCalibr_Click);
            // 
            // tbPixelGain
            // 
            this.tbPixelGain.Location = new System.Drawing.Point(10, 41);
            this.tbPixelGain.Name = "tbPixelGain";
            this.tbPixelGain.Size = new System.Drawing.Size(61, 20);
            this.tbPixelGain.TabIndex = 6;
            this.tbPixelGain.TextChanged += new System.EventHandler(this.TextChanged);
            this.tbPixelGain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // tbPixelOffset
            // 
            this.tbPixelOffset.Location = new System.Drawing.Point(10, 71);
            this.tbPixelOffset.Name = "tbPixelOffset";
            this.tbPixelOffset.Size = new System.Drawing.Size(61, 20);
            this.tbPixelOffset.TabIndex = 8;
            this.tbPixelOffset.TextChanged += new System.EventHandler(this.TextChanged);
            this.tbPixelOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // tbValueOffset
            // 
            this.tbValueOffset.Location = new System.Drawing.Point(77, 71);
            this.tbValueOffset.Name = "tbValueOffset";
            this.tbValueOffset.Size = new System.Drawing.Size(61, 20);
            this.tbValueOffset.TabIndex = 9;
            this.tbValueOffset.TextChanged += new System.EventHandler(this.TextChanged);
            this.tbValueOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // tbValueGain
            // 
            this.tbValueGain.Location = new System.Drawing.Point(78, 41);
            this.tbValueGain.Name = "tbValueGain";
            this.tbValueGain.Size = new System.Drawing.Size(61, 20);
            this.tbValueGain.TabIndex = 7;
            this.tbValueGain.TextChanged += new System.EventHandler(this.TextChanged);
            this.tbValueGain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPress);
            // 
            // btOffseCalibr
            // 
            this.btOffseCalibr.Location = new System.Drawing.Point(145, 68);
            this.btOffseCalibr.Name = "btOffseCalibr";
            this.btOffseCalibr.Size = new System.Drawing.Size(55, 23);
            this.btOffseCalibr.TabIndex = 10;
            this.btOffseCalibr.Text = "offset";
            this.btOffseCalibr.UseVisualStyleBackColor = true;
            this.btOffseCalibr.Click += new System.EventHandler(this.btOffseCalibr_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "pixel:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "value:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "set:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btGainCalibr);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbPixelGain);
            this.groupBox1.Controls.Add(this.tbPixelOffset);
            this.groupBox1.Controls.Add(this.btOffseCalibr);
            this.groupBox1.Controls.Add(this.tbValueGain);
            this.groupBox1.Controls.Add(this.tbValueOffset);
            this.groupBox1.Location = new System.Drawing.Point(293, 411);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 117);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "configure pixel";
            // 
            // rchtbMonCmd
            // 
            this.rchtbMonCmd.BackColor = System.Drawing.Color.White;
            this.rchtbMonCmd.Location = new System.Drawing.Point(43, 303);
            this.rchtbMonCmd.Name = "rchtbMonCmd";
            this.rchtbMonCmd.ReadOnly = true;
            this.rchtbMonCmd.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rchtbMonCmd.Size = new System.Drawing.Size(243, 102);
            this.rchtbMonCmd.TabIndex = 15;
            this.rchtbMonCmd.Text = "";
            this.rchtbMonCmd.TextChanged += new System.EventHandler(this.rchtbMonCmd_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "cmd mon:";
            // 
            // btReadGainOffset
            // 
            this.btReadGainOffset.Location = new System.Drawing.Point(6, 48);
            this.btReadGainOffset.Name = "btReadGainOffset";
            this.btReadGainOffset.Size = new System.Drawing.Size(188, 23);
            this.btReadGainOffset.TabIndex = 22;
            this.btReadGainOffset.Text = "read gain and offset";
            this.btReadGainOffset.UseVisualStyleBackColor = true;
            this.btReadGainOffset.Click += new System.EventHandler(this.btReadGainOffset_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 49);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(71, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "emulation";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(196, 25);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 17);
            this.checkBox2.TabIndex = 21;
            this.checkBox2.Text = "logging";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.rbStatic);
            this.groupBox3.Controls.Add(this.rbRealTime);
            this.groupBox3.Location = new System.Drawing.Point(293, 534);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(262, 45);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "snap mode";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(125, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // rbStatic
            // 
            this.rbStatic.AutoSize = true;
            this.rbStatic.Location = new System.Drawing.Point(76, 18);
            this.rbStatic.Name = "rbStatic";
            this.rbStatic.Size = new System.Drawing.Size(50, 17);
            this.rbStatic.TabIndex = 1;
            this.rbStatic.TabStop = true;
            this.rbStatic.Text = "static";
            this.rbStatic.UseVisualStyleBackColor = true;
            this.rbStatic.CheckedChanged += new System.EventHandler(this.rbStatic_CheckedChanged);
            // 
            // rbRealTime
            // 
            this.rbRealTime.AutoSize = true;
            this.rbRealTime.Location = new System.Drawing.Point(6, 18);
            this.rbRealTime.Name = "rbRealTime";
            this.rbRealTime.Size = new System.Drawing.Size(64, 17);
            this.rbRealTime.TabIndex = 0;
            this.rbRealTime.TabStop = true;
            this.rbRealTime.Text = "real time";
            this.rbRealTime.UseVisualStyleBackColor = true;
            this.rbRealTime.CheckedChanged += new System.EventHandler(this.rbRealTime_CheckedChanged);
            // 
            // lvPixelInfo
            // 
            this.lvPixelInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvPixelInfo.Location = new System.Drawing.Point(575, 9);
            this.lvPixelInfo.Name = "lvPixelInfo";
            this.lvPixelInfo.Size = new System.Drawing.Size(245, 570);
            this.lvPixelInfo.TabIndex = 23;
            this.lvPixelInfo.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "pixel";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "value";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "gain";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "offset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "65535";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(544, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "512";
            // 
            // tbCommand
            // 
            this.tbCommand.Location = new System.Drawing.Point(6, 91);
            this.tbCommand.Name = "tbCommand";
            this.tbCommand.Size = new System.Drawing.Size(220, 20);
            this.tbCommand.TabIndex = 28;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btStopFrame);
            this.groupBox4.Controls.Add(this.btStartFrame);
            this.groupBox4.Controls.Add(this.btnSnap);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.checkBox2);
            this.groupBox4.Controls.Add(this.btSend);
            this.groupBox4.Controls.Add(this.tbCommand);
            this.groupBox4.Location = new System.Drawing.Point(293, 283);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(262, 122);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "panel";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "handle input command";
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(232, 89);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(24, 23);
            this.btSend.TabIndex = 14;
            this.btSend.Text = ">";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // btResetGainOffset
            // 
            this.btResetGainOffset.Location = new System.Drawing.Point(7, 19);
            this.btResetGainOffset.Name = "btResetGainOffset";
            this.btResetGainOffset.Size = new System.Drawing.Size(187, 23);
            this.btResetGainOffset.TabIndex = 14;
            this.btResetGainOffset.Text = "reset gain and offset";
            this.btResetGainOffset.UseVisualStyleBackColor = true;
            this.btResetGainOffset.Click += new System.EventHandler(this.btResetGainOffset_Click);
            // 
            // tbNormVal
            // 
            this.tbNormVal.Location = new System.Drawing.Point(111, 75);
            this.tbNormVal.Name = "tbNormVal";
            this.tbNormVal.Size = new System.Drawing.Size(84, 20);
            this.tbNormVal.TabIndex = 30;
            this.tbNormVal.Text = "60000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "normalization value:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.tbNormVal);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.btWriteNewG_O);
            this.groupBox5.Controls.Add(this.btCalcG_O);
            this.groupBox5.Controls.Add(this.btReadGainOffset);
            this.groupBox5.Controls.Add(this.btResetGainOffset);
            this.groupBox5.Location = new System.Drawing.Point(43, 411);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(244, 168);
            this.groupBox5.TabIndex = 31;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "calibration panel";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(201, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "label14";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(201, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "label13";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(200, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "label12";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(200, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "label11";
            // 
            // btWriteNewG_O
            // 
            this.btWriteNewG_O.Location = new System.Drawing.Point(7, 130);
            this.btWriteNewG_O.Name = "btWriteNewG_O";
            this.btWriteNewG_O.Size = new System.Drawing.Size(188, 23);
            this.btWriteNewG_O.TabIndex = 24;
            this.btWriteNewG_O.Text = "write gain and offset";
            this.btWriteNewG_O.UseVisualStyleBackColor = true;
            this.btWriteNewG_O.Click += new System.EventHandler(this.btWriteNewG_O_Click);
            // 
            // btCalcG_O
            // 
            this.btCalcG_O.Location = new System.Drawing.Point(7, 101);
            this.btCalcG_O.Name = "btCalcG_O";
            this.btCalcG_O.Size = new System.Drawing.Size(188, 23);
            this.btCalcG_O.TabIndex = 23;
            this.btCalcG_O.Text = "calc new gain for each pixel ";
            this.btCalcG_O.UseVisualStyleBackColor = true;
            this.btCalcG_O.Click += new System.EventHandler(this.btCalcG_O_Click);
            // 
            // btStartFrame
            // 
            this.btStartFrame.Location = new System.Drawing.Point(95, 21);
            this.btStartFrame.Name = "btStartFrame";
            this.btStartFrame.Size = new System.Drawing.Size(79, 23);
            this.btStartFrame.TabIndex = 30;
            this.btStartFrame.Text = "start frame";
            this.btStartFrame.UseVisualStyleBackColor = true;
            this.btStartFrame.Click += new System.EventHandler(this.btStartFrame_Click);
            // 
            // btStopFrame
            // 
            this.btStopFrame.Location = new System.Drawing.Point(95, 49);
            this.btStopFrame.Name = "btStopFrame";
            this.btStopFrame.Size = new System.Drawing.Size(79, 23);
            this.btStopFrame.TabIndex = 31;
            this.btStopFrame.Text = "stop frame";
            this.btStopFrame.UseVisualStyleBackColor = true;
            this.btStopFrame.Click += new System.EventHandler(this.btStopFrame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 588);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvPixelInfo);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rchtbMonCmd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "TestDetectorCalibration";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSnap;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btGainCalibr;
        private System.Windows.Forms.TextBox tbPixelGain;
        private System.Windows.Forms.TextBox tbPixelOffset;
        private System.Windows.Forms.TextBox tbValueOffset;
        private System.Windows.Forms.TextBox tbValueGain;
        private System.Windows.Forms.Button btOffseCalibr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rchtbMonCmd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rbStatic;
        private System.Windows.Forms.RadioButton rbRealTime;
        private System.Windows.Forms.ListView lvPixelInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btReadGainOffset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbCommand;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btResetGainOffset;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbNormVal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btWriteNewG_O;
        private System.Windows.Forms.Button btCalcG_O;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btStopFrame;
        private System.Windows.Forms.Button btStartFrame;

    }
}

