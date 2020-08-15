namespace WorkWithMemory
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
            this.tbStructMem = new System.Windows.Forms.TextBox();
            this.tbDumpMem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReadMem = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Hint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ascii = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IntBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rbSpace = new System.Windows.Forms.RadioButton();
            this.rbStream = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbLocalIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMask = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbGateway = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbMac = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDnsIP = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.ch1 = new System.Windows.Forms.TabPage();
            this.ch2 = new System.Windows.Forms.TabPage();
            this.ch3 = new System.Windows.Forms.TabPage();
            this.ch4 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.tbFirmware = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbLocalPortCH1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbDomainCH1 = new System.Windows.Forms.TextBox();
            this.tbRemoteIPCH1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRemotePortCH1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.ch1.SuspendLayout();
            this.ch2.SuspendLayout();
            this.ch3.SuspendLayout();
            this.ch4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbStructMem
            // 
            this.tbStructMem.Location = new System.Drawing.Point(12, 53);
            this.tbStructMem.Multiline = true;
            this.tbStructMem.Name = "tbStructMem";
            this.tbStructMem.Size = new System.Drawing.Size(180, 241);
            this.tbStructMem.TabIndex = 0;
            // 
            // tbDumpMem
            // 
            this.tbDumpMem.Location = new System.Drawing.Point(12, 323);
            this.tbDumpMem.Multiline = true;
            this.tbDumpMem.Name = "tbDumpMem";
            this.tbDumpMem.Size = new System.Drawing.Size(180, 364);
            this.tbDumpMem.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Structure Memory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Damp Memory";
            // 
            // btnReadMem
            // 
            this.btnReadMem.Location = new System.Drawing.Point(16, 693);
            this.btnReadMem.Name = "btnReadMem";
            this.btnReadMem.Size = new System.Drawing.Size(177, 23);
            this.btnReadMem.TabIndex = 4;
            this.btnReadMem.Text = "Read Memory";
            this.btnReadMem.UseVisualStyleBackColor = true;
            this.btnReadMem.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Hint,
            this.HexBytes,
            this.ascii,
            this.value,
            this.IntBytes});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(6, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1419, 663);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Hint
            // 
            this.Hint.Text = "Hint";
            // 
            // HexBytes
            // 
            this.HexBytes.Text = "Hex Bytes";
            this.HexBytes.Width = 500;
            // 
            // ascii
            // 
            this.ascii.Text = "ASCII";
            this.ascii.Width = 200;
            // 
            // value
            // 
            this.value.Text = "Digital Value";
            this.value.Width = 150;
            // 
            // IntBytes
            // 
            this.IntBytes.Text = "Int Bytes";
            this.IntBytes.Width = 500;
            // 
            // rbSpace
            // 
            this.rbSpace.AutoSize = true;
            this.rbSpace.Checked = true;
            this.rbSpace.Location = new System.Drawing.Point(90, 300);
            this.rbSpace.Name = "rbSpace";
            this.rbSpace.Size = new System.Drawing.Size(54, 17);
            this.rbSpace.TabIndex = 6;
            this.rbSpace.TabStop = true;
            this.rbSpace.Text = "space";
            this.rbSpace.UseVisualStyleBackColor = true;
            // 
            // rbStream
            // 
            this.rbStream.AutoSize = true;
            this.rbStream.Location = new System.Drawing.Point(150, 300);
            this.rbStream.Name = "rbStream";
            this.rbStream.Size = new System.Drawing.Size(56, 17);
            this.rbStream.TabIndex = 7;
            this.rbStream.Text = "stream";
            this.rbStream.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(211, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1443, 703);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1435, 677);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dump Memory";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Menu;
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.tbFirmware);
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.tbDnsIP);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.tbMac);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.tbGateway);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tbMask);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.tbLocalIP);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1435, 677);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fields Settings";
            // 
            // tbLocalIP
            // 
            this.tbLocalIP.Location = new System.Drawing.Point(77, 103);
            this.tbLocalIP.Name = "tbLocalIP";
            this.tbLocalIP.Size = new System.Drawing.Size(100, 20);
            this.tbLocalIP.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Local IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mask";
            // 
            // tbMask
            // 
            this.tbMask.Location = new System.Drawing.Point(77, 129);
            this.tbMask.Name = "tbMask";
            this.tbMask.Size = new System.Drawing.Size(100, 20);
            this.tbMask.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Gateway";
            // 
            // tbGateway
            // 
            this.tbGateway.Location = new System.Drawing.Point(77, 155);
            this.tbGateway.Name = "tbGateway";
            this.tbGateway.Size = new System.Drawing.Size(100, 20);
            this.tbGateway.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "MAC";
            // 
            // tbMac
            // 
            this.tbMac.Location = new System.Drawing.Point(77, 77);
            this.tbMac.Name = "tbMac";
            this.tbMac.Size = new System.Drawing.Size(100, 20);
            this.tbMac.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "DNS IP";
            // 
            // tbDnsIP
            // 
            this.tbDnsIP.Location = new System.Drawing.Point(77, 181);
            this.tbDnsIP.Name = "tbDnsIP";
            this.tbDnsIP.Size = new System.Drawing.Size(100, 20);
            this.tbDnsIP.TabIndex = 8;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.ch1);
            this.tabControl2.Controls.Add(this.ch2);
            this.tabControl2.Controls.Add(this.ch3);
            this.tabControl2.Controls.Add(this.ch4);
            this.tabControl2.Location = new System.Drawing.Point(39, 332);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(699, 292);
            this.tabControl2.TabIndex = 10;
            // 
            // ch1
            // 
            this.ch1.BackColor = System.Drawing.SystemColors.Menu;
            this.ch1.Controls.Add(this.label12);
            this.ch1.Controls.Add(this.tbRemotePortCH1);
            this.ch1.Controls.Add(this.tbRemoteIPCH1);
            this.ch1.Controls.Add(this.tbDomainCH1);
            this.ch1.Controls.Add(this.tbLocalPortCH1);
            this.ch1.Controls.Add(this.label9);
            this.ch1.Controls.Add(this.label10);
            this.ch1.Controls.Add(this.label11);
            this.ch1.Location = new System.Drawing.Point(4, 22);
            this.ch1.Name = "ch1";
            this.ch1.Padding = new System.Windows.Forms.Padding(3);
            this.ch1.Size = new System.Drawing.Size(691, 266);
            this.ch1.TabIndex = 0;
            this.ch1.Text = "Channel #1";
            // 
            // ch2
            // 
            this.ch2.BackColor = System.Drawing.SystemColors.Menu;
            this.ch2.Controls.Add(this.label13);
            this.ch2.Controls.Add(this.textBox11);
            this.ch2.Controls.Add(this.label14);
            this.ch2.Controls.Add(this.textBox12);
            this.ch2.Controls.Add(this.textBox13);
            this.ch2.Controls.Add(this.label15);
            this.ch2.Controls.Add(this.label16);
            this.ch2.Controls.Add(this.textBox14);
            this.ch2.Location = new System.Drawing.Point(4, 22);
            this.ch2.Name = "ch2";
            this.ch2.Padding = new System.Windows.Forms.Padding(3);
            this.ch2.Size = new System.Drawing.Size(691, 266);
            this.ch2.TabIndex = 1;
            this.ch2.Text = "Channel #2";
            // 
            // ch3
            // 
            this.ch3.BackColor = System.Drawing.SystemColors.Menu;
            this.ch3.Controls.Add(this.label17);
            this.ch3.Controls.Add(this.textBox15);
            this.ch3.Controls.Add(this.textBox16);
            this.ch3.Controls.Add(this.textBox17);
            this.ch3.Controls.Add(this.textBox18);
            this.ch3.Controls.Add(this.label18);
            this.ch3.Controls.Add(this.label19);
            this.ch3.Controls.Add(this.label20);
            this.ch3.Location = new System.Drawing.Point(4, 22);
            this.ch3.Name = "ch3";
            this.ch3.Size = new System.Drawing.Size(691, 266);
            this.ch3.TabIndex = 2;
            this.ch3.Text = "Channel #3";
            // 
            // ch4
            // 
            this.ch4.BackColor = System.Drawing.SystemColors.Menu;
            this.ch4.Controls.Add(this.label21);
            this.ch4.Controls.Add(this.textBox19);
            this.ch4.Controls.Add(this.textBox20);
            this.ch4.Controls.Add(this.textBox21);
            this.ch4.Controls.Add(this.textBox22);
            this.ch4.Controls.Add(this.label22);
            this.ch4.Controls.Add(this.label23);
            this.ch4.Controls.Add(this.label24);
            this.ch4.Location = new System.Drawing.Point(4, 22);
            this.ch4.Name = "ch4";
            this.ch4.Size = new System.Drawing.Size(691, 266);
            this.ch4.TabIndex = 3;
            this.ch4.Text = "Channel #4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Firmware";
            // 
            // tbFirmware
            // 
            this.tbFirmware.Location = new System.Drawing.Point(77, 51);
            this.tbFirmware.Name = "tbFirmware";
            this.tbFirmware.Size = new System.Drawing.Size(100, 20);
            this.tbFirmware.TabIndex = 11;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(246, 77);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(100, 20);
            this.textBox14.TabIndex = 23;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(31, 61);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Domain";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(243, 61);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Local port";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(34, 77);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(177, 20);
            this.textBox13.TabIndex = 21;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(352, 77);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 20);
            this.textBox12.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(349, 61);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "Remote IP";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(458, 77);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(100, 20);
            this.textBox11.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(455, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Remote port";
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(246, 62);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(100, 20);
            this.textBox22.TabIndex = 23;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(31, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(43, 13);
            this.label24.TabIndex = 22;
            this.label24.Text = "Domain";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(243, 46);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(54, 13);
            this.label23.TabIndex = 24;
            this.label23.Text = "Local port";
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(34, 62);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(177, 20);
            this.textBox21.TabIndex = 21;
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(352, 62);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(100, 20);
            this.textBox20.TabIndex = 25;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(349, 46);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(57, 13);
            this.label22.TabIndex = 26;
            this.label22.Text = "Remote IP";
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(458, 62);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(100, 20);
            this.textBox19.TabIndex = 27;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(455, 46);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 13);
            this.label21.TabIndex = 28;
            this.label21.Text = "Remote port";
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(246, 67);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(100, 20);
            this.textBox18.TabIndex = 23;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(31, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 13);
            this.label20.TabIndex = 22;
            this.label20.Text = "Domain";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(243, 51);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(54, 13);
            this.label19.TabIndex = 24;
            this.label19.Text = "Local port";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(34, 67);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(177, 20);
            this.textBox17.TabIndex = 21;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(352, 67);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(100, 20);
            this.textBox16.TabIndex = 25;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(349, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 26;
            this.label18.Text = "Remote IP";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(458, 67);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(100, 20);
            this.textBox15.TabIndex = 27;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(455, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "Remote port";
            // 
            // tbLocalPortCH1
            // 
            this.tbLocalPortCH1.Location = new System.Drawing.Point(221, 64);
            this.tbLocalPortCH1.Name = "tbLocalPortCH1";
            this.tbLocalPortCH1.Size = new System.Drawing.Size(100, 20);
            this.tbLocalPortCH1.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Domain";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(218, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Local port";
            // 
            // tbDomainCH1
            // 
            this.tbDomainCH1.Location = new System.Drawing.Point(9, 64);
            this.tbDomainCH1.Name = "tbDomainCH1";
            this.tbDomainCH1.Size = new System.Drawing.Size(177, 20);
            this.tbDomainCH1.TabIndex = 13;
            // 
            // tbRemoteIPCH1
            // 
            this.tbRemoteIPCH1.Location = new System.Drawing.Point(327, 64);
            this.tbRemoteIPCH1.Name = "tbRemoteIPCH1";
            this.tbRemoteIPCH1.Size = new System.Drawing.Size(100, 20);
            this.tbRemoteIPCH1.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(324, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Remote IP";
            // 
            // tbRemotePortCH1
            // 
            this.tbRemotePortCH1.Location = new System.Drawing.Point(433, 64);
            this.tbRemotePortCH1.Name = "tbRemotePortCH1";
            this.tbRemotePortCH1.Size = new System.Drawing.Size(100, 20);
            this.tbRemotePortCH1.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(430, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Remote port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1688, 743);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.rbStream);
            this.Controls.Add(this.rbSpace);
            this.Controls.Add(this.btnReadMem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDumpMem);
            this.Controls.Add(this.tbStructMem);
            this.Name = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.ch1.ResumeLayout(false);
            this.ch1.PerformLayout();
            this.ch2.ResumeLayout(false);
            this.ch2.PerformLayout();
            this.ch3.ResumeLayout(false);
            this.ch3.PerformLayout();
            this.ch4.ResumeLayout(false);
            this.ch4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbStructMem;
        private System.Windows.Forms.TextBox tbDumpMem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReadMem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Hint;
        private System.Windows.Forms.ColumnHeader HexBytes;
        private System.Windows.Forms.ColumnHeader ascii;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.ColumnHeader IntBytes;
        private System.Windows.Forms.RadioButton rbSpace;
        private System.Windows.Forms.RadioButton rbStream;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage ch1;
        private System.Windows.Forms.TabPage ch2;
        private System.Windows.Forms.TabPage ch3;
        private System.Windows.Forms.TabPage ch4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDnsIP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbMac;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbGateway;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLocalIP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbFirmware;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbRemotePortCH1;
        private System.Windows.Forms.TextBox tbRemoteIPCH1;
        private System.Windows.Forms.TextBox tbDomainCH1;
        private System.Windows.Forms.TextBox tbLocalPortCH1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;

    }
}

