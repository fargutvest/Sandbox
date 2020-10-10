namespace Tolkit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSend1 = new System.Windows.Forms.Button();
            this.tbSend1 = new System.Windows.Forms.TextBox();
            this.rchtbTranceive = new System.Windows.Forms.RichTextBox();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.tbSend2 = new System.Windows.Forms.TextBox();
            this.btnSend2 = new System.Windows.Forms.Button();
            this.tbSend3 = new System.Windows.Forms.TextBox();
            this.btnSend3 = new System.Windows.Forms.Button();
            this.tbSend4 = new System.Windows.Forms.TextBox();
            this.btnSend4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBaudRate = new System.Windows.Forms.TextBox();
            this.rbUdp = new System.Windows.Forms.RadioButton();
            this.rbCom = new System.Windows.Forms.RadioButton();
            this.cbCom = new System.Windows.Forms.ComboBox();
            this.chbMacros = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.rchtbReceive = new System.Windows.Forms.RichTextBox();
            this.chbDtr = new System.Windows.Forms.CheckBox();
            this.chbRts = new System.Windows.Forms.CheckBox();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.listMacros = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rbTcp = new System.Windows.Forms.RadioButton();
            this.lbRotate = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.selectEncodingReceive = new Tolkit.controls.SelectEncoding();
            this.selectEncodingTranceive = new Tolkit.controls.SelectEncoding();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend1
            // 
            this.btnSend1.Location = new System.Drawing.Point(176, 167);
            this.btnSend1.Name = "btnSend1";
            this.btnSend1.Size = new System.Drawing.Size(30, 23);
            this.btnSend1.TabIndex = 0;
            this.btnSend1.Text = ">>";
            this.btnSend1.UseVisualStyleBackColor = true;
            this.btnSend1.Click += new System.EventHandler(this.btnSend_Click);
            this.btnSend1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            // 
            // tbSend1
            // 
            this.tbSend1.Location = new System.Drawing.Point(12, 169);
            this.tbSend1.Name = "tbSend1";
            this.tbSend1.Size = new System.Drawing.Size(158, 20);
            this.tbSend1.TabIndex = 1;
            // 
            // rchtbTranceive
            // 
            this.rchtbTranceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchtbTranceive.Location = new System.Drawing.Point(3, 3);
            this.rchtbTranceive.Name = "rchtbTranceive";
            this.rchtbTranceive.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rchtbTranceive.Size = new System.Drawing.Size(259, 102);
            this.rchtbTranceive.TabIndex = 2;
            this.rchtbTranceive.Text = "";
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(12, 32);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(100, 20);
            this.tbIp.TabIndex = 3;
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(118, 32);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(45, 20);
            this.tbPort.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(229, 106);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(30, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "c";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            this.btnConnect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(12, 148);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(0, 13);
            this.lblHint.TabIndex = 8;
            // 
            // tbSend2
            // 
            this.tbSend2.Location = new System.Drawing.Point(12, 195);
            this.tbSend2.Name = "tbSend2";
            this.tbSend2.Size = new System.Drawing.Size(158, 20);
            this.tbSend2.TabIndex = 10;
            // 
            // btnSend2
            // 
            this.btnSend2.Location = new System.Drawing.Point(176, 193);
            this.btnSend2.Name = "btnSend2";
            this.btnSend2.Size = new System.Drawing.Size(30, 23);
            this.btnSend2.TabIndex = 9;
            this.btnSend2.Text = ">>";
            this.btnSend2.UseVisualStyleBackColor = true;
            this.btnSend2.Click += new System.EventHandler(this.btnSend2_Click);
            this.btnSend2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            // 
            // tbSend3
            // 
            this.tbSend3.Location = new System.Drawing.Point(12, 221);
            this.tbSend3.Name = "tbSend3";
            this.tbSend3.Size = new System.Drawing.Size(158, 20);
            this.tbSend3.TabIndex = 12;
            // 
            // btnSend3
            // 
            this.btnSend3.Location = new System.Drawing.Point(176, 219);
            this.btnSend3.Name = "btnSend3";
            this.btnSend3.Size = new System.Drawing.Size(30, 23);
            this.btnSend3.TabIndex = 11;
            this.btnSend3.Text = ">>";
            this.btnSend3.UseVisualStyleBackColor = true;
            this.btnSend3.Click += new System.EventHandler(this.btnSend3_Click);
            this.btnSend3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            // 
            // tbSend4
            // 
            this.tbSend4.Location = new System.Drawing.Point(12, 247);
            this.tbSend4.Name = "tbSend4";
            this.tbSend4.Size = new System.Drawing.Size(158, 20);
            this.tbSend4.TabIndex = 14;
            // 
            // btnSend4
            // 
            this.btnSend4.Location = new System.Drawing.Point(176, 245);
            this.btnSend4.Name = "btnSend4";
            this.btnSend4.Size = new System.Drawing.Size(30, 23);
            this.btnSend4.TabIndex = 13;
            this.btnSend4.Text = ">>";
            this.btnSend4.UseVisualStyleBackColor = true;
            this.btnSend4.Click += new System.EventHandler(this.btnSend4_Click);
            this.btnSend4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "BaudRate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Com";
            // 
            // tbBaudRate
            // 
            this.tbBaudRate.Location = new System.Drawing.Point(118, 79);
            this.tbBaudRate.Name = "tbBaudRate";
            this.tbBaudRate.Size = new System.Drawing.Size(100, 20);
            this.tbBaudRate.TabIndex = 18;
            // 
            // rbUdp
            // 
            this.rbUdp.AutoSize = true;
            this.rbUdp.Location = new System.Drawing.Point(226, 29);
            this.rbUdp.Name = "rbUdp";
            this.rbUdp.Size = new System.Drawing.Size(69, 17);
            this.rbUdp.TabIndex = 22;
            this.rbUdp.Text = "UDP port";
            this.rbUdp.UseVisualStyleBackColor = true;
            this.rbUdp.CheckedChanged += new System.EventHandler(this.rbUdp_CheckedChanged);
            // 
            // rbCom
            // 
            this.rbCom.AutoSize = true;
            this.rbCom.Checked = true;
            this.rbCom.Location = new System.Drawing.Point(226, 81);
            this.rbCom.Name = "rbCom";
            this.rbCom.Size = new System.Drawing.Size(70, 17);
            this.rbCom.TabIndex = 23;
            this.rbCom.TabStop = true;
            this.rbCom.Text = "COM port";
            this.rbCom.UseVisualStyleBackColor = true;
            this.rbCom.CheckedChanged += new System.EventHandler(this.rbCom_CheckedChanged);
            // 
            // cbCom
            // 
            this.cbCom.FormattingEnabled = true;
            this.cbCom.Location = new System.Drawing.Point(12, 78);
            this.cbCom.Name = "cbCom";
            this.cbCom.Size = new System.Drawing.Size(100, 21);
            this.cbCom.TabIndex = 25;
            this.cbCom.DropDown += new System.EventHandler(this.cbCom_DropDown);
            // 
            // chbMacros
            // 
            this.chbMacros.AutoSize = true;
            this.chbMacros.Location = new System.Drawing.Point(460, 9);
            this.chbMacros.Name = "chbMacros";
            this.chbMacros.Size = new System.Drawing.Size(82, 17);
            this.chbMacros.TabIndex = 29;
            this.chbMacros.Text = "Use macros";
            this.chbMacros.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(403, 280);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 23);
            this.btnClear.TabIndex = 28;
            this.btnClear.Text = "clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rchtbReceive
            // 
            this.rchtbReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchtbReceive.Location = new System.Drawing.Point(268, 3);
            this.rchtbReceive.Name = "rchtbReceive";
            this.rchtbReceive.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rchtbReceive.Size = new System.Drawing.Size(259, 102);
            this.rchtbReceive.TabIndex = 30;
            this.rchtbReceive.Text = "";
            // 
            // chbDtr
            // 
            this.chbDtr.AutoSize = true;
            this.chbDtr.Location = new System.Drawing.Point(12, 106);
            this.chbDtr.Name = "chbDtr";
            this.chbDtr.Size = new System.Drawing.Size(38, 17);
            this.chbDtr.TabIndex = 33;
            this.chbDtr.Text = "dtr";
            this.chbDtr.UseVisualStyleBackColor = true;
            this.chbDtr.CheckedChanged += new System.EventHandler(this.chbDtr_CheckedChanged);
            // 
            // chbRts
            // 
            this.chbRts.AutoSize = true;
            this.chbRts.Location = new System.Drawing.Point(47, 105);
            this.chbRts.Name = "chbRts";
            this.chbRts.Size = new System.Drawing.Size(37, 17);
            this.chbRts.TabIndex = 34;
            this.chbRts.Text = "rts";
            this.chbRts.UseVisualStyleBackColor = true;
            this.chbRts.CheckedChanged += new System.EventHandler(this.chbRts_CheckedChanged);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Location = new System.Drawing.Point(263, 106);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(30, 23);
            this.btDisconnect.TabIndex = 36;
            this.btDisconnect.Text = "d";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // listMacros
            // 
            this.listMacros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMacros.HideSelection = false;
            this.listMacros.Location = new System.Drawing.Point(332, 29);
            this.listMacros.MultiSelect = false;
            this.listMacros.Name = "listMacros";
            this.listMacros.Size = new System.Drawing.Size(210, 143);
            this.listMacros.TabIndex = 37;
            this.listMacros.UseCompatibleStateImageBehavior = false;
            this.listMacros.View = System.Windows.Forms.View.List;
            this.listMacros.SelectedIndexChanged += new System.EventHandler(this.listMacros_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(329, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Macroses:";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Location = new System.Drawing.Point(168, 32);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(45, 20);
            this.tbLocalPort.TabIndex = 41;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(165, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "LocalPort";
            // 
            // rbTcp
            // 
            this.rbTcp.AutoSize = true;
            this.rbTcp.Location = new System.Drawing.Point(226, 55);
            this.rbTcp.Name = "rbTcp";
            this.rbTcp.Size = new System.Drawing.Size(67, 17);
            this.rbTcp.TabIndex = 43;
            this.rbTcp.Text = "TCP port";
            this.rbTcp.UseVisualStyleBackColor = true;
            this.rbTcp.CheckedChanged += new System.EventHandler(this.rbTcp_CheckedChanged);
            // 
            // lbRotate
            // 
            this.lbRotate.AutoSize = true;
            this.lbRotate.Location = new System.Drawing.Point(147, 13);
            this.lbRotate.Name = "lbRotate";
            this.lbRotate.Size = new System.Drawing.Size(22, 13);
            this.lbRotate.TabIndex = 46;
            this.lbRotate.Text = "<->";
            this.lbRotate.Click += new System.EventHandler(this.lbRotate_Click);
            this.lbRotate.MouseLeave += new System.EventHandler(this.lbRotate_MouseLeave);
            this.lbRotate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbRotate_MouseMove);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.rchtbTranceive, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rchtbReceive, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 306);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(530, 108);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // selectEncodingReceive
            // 
            this.selectEncodingReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectEncodingReceive.Location = new System.Drawing.Point(476, 234);
            this.selectEncodingReceive.Name = "selectEncodingReceive";
            this.selectEncodingReceive.Size = new System.Drawing.Size(66, 66);
            this.selectEncodingReceive.TabIndex = 45;
            // 
            // selectEncodingTranceive
            // 
            this.selectEncodingTranceive.Location = new System.Drawing.Point(212, 167);
            this.selectEncodingTranceive.Name = "selectEncodingTranceive";
            this.selectEncodingTranceive.Size = new System.Drawing.Size(66, 66);
            this.selectEncodingTranceive.TabIndex = 44;
            this.selectEncodingTranceive.ChekedChanged += new System.EventHandler(this.selectEncodingTranceive_ChekedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 426);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lbRotate);
            this.Controls.Add(this.selectEncodingReceive);
            this.Controls.Add(this.selectEncodingTranceive);
            this.Controls.Add(this.chbMacros);
            this.Controls.Add(this.rbTcp);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tbLocalPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listMacros);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.chbRts);
            this.Controls.Add(this.chbDtr);
            this.Controls.Add(this.cbCom);
            this.Controls.Add(this.rbCom);
            this.Controls.Add(this.rbUdp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbBaudRate);
            this.Controls.Add(this.tbSend4);
            this.Controls.Add(this.btnSend4);
            this.Controls.Add(this.tbSend3);
            this.Controls.Add(this.btnSend3);
            this.Controls.Add(this.tbSend2);
            this.Controls.Add(this.btnSend2);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.tbSend1);
            this.Controls.Add(this.btnSend1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(566, 464);
            this.Name = "Form1";
            this.Text = "Tolkit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend1;
        private System.Windows.Forms.TextBox tbSend1;
        private System.Windows.Forms.RichTextBox rchtbTranceive;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.TextBox tbSend2;
        private System.Windows.Forms.Button btnSend2;
        private System.Windows.Forms.TextBox tbSend3;
        private System.Windows.Forms.Button btnSend3;
        private System.Windows.Forms.TextBox tbSend4;
        private System.Windows.Forms.Button btnSend4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBaudRate;
        private System.Windows.Forms.RadioButton rbUdp;
        private System.Windows.Forms.RadioButton rbCom;
        private System.Windows.Forms.ComboBox cbCom;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chbMacros;
        private System.Windows.Forms.RichTextBox rchtbReceive;
        private System.Windows.Forms.CheckBox chbDtr;
        private System.Windows.Forms.CheckBox chbRts;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.ListView listMacros;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbTcp;
        private controls.SelectEncoding selectEncodingTranceive;
        private controls.SelectEncoding selectEncodingReceive;
        private System.Windows.Forms.Label lbRotate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

