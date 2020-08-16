using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tolkit
{
    public partial class Form1 : Form
    {

        ComPort comport;
        TcpPortClient tcpport;
        UdpPort udpport;

        IPortGeneral port;
        string pathMemory = "memory";

        public Form1()
        {
            InitializeComponent();
            rbUdp.Checked = true;
            MacrosSelect.Instance.SendingMessage += macros_evSendMessage;
        }


        #region buttons_click
        private void btnSend_Click(object sender, EventArgs e)
        {
            BlinkButton.Instance.Blink(sender, Send(stringToByteArr(tbSend1.Text)));
        }

        private void btnSend2_Click(object sender, EventArgs e)
        {
            BlinkButton.Instance.Blink(sender, Send(stringToByteArr(tbSend2.Text)));
        }

        private void btnSend3_Click(object sender, EventArgs e)
        {
            BlinkButton.Instance.Blink(sender, Send(stringToByteArr(tbSend3.Text)));
        }

        private void btnSend4_Click(object sender, EventArgs e)
        {
            BlinkButton.Instance.Blink(sender, Send(stringToByteArr(tbSend4.Text)));
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbCom.Checked)
                {
                    comport = new ComPort(cbCom.Text, Convert.ToInt32(tbBaudRate.Text));
                    port = comport;
                }
                if (rbUdp.Checked)
                {
                    udpport = new UdpPort(tbIp.Text, Convert.ToInt32(tbPort.Text), Convert.ToInt32(tbLocalPort.Text));
                    port = udpport;
                }
                if (rbTcp.Checked)
                {
                    tcpport = new TcpPortClient(tbIp.Text, Convert.ToUInt16(tbPort.Text));//, 3000, 3000);
                    port = tcpport;
                }

                port.DataReceived += portDataReceivedEventhandler;
                port.Open();

                if (port.IsOpen)
                    btnConnect.BackColor = Color.LightGreen;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            if (port != null)
            {
                port.DataReceived -= portDataReceivedEventhandler;
                port.Close();
                port.Dispose();
            }
            btnConnect.BackColor = SystemColors.Control;

        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = SystemColors.Menu;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rchtbTranceive.Text = "";
            rchtbReceive.Text = "";
        }
        #endregion

        #region eventhandlers
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, object> kvp in MacrosSelect.Instance.Macroses)
            {
                listMacros.Items.Add(kvp.Key);
            }

            MemoryControlsInfo.Instance.Add(selectEncodingTranceive.rbHex);
            MemoryControlsInfo.Instance.Add(selectEncodingTranceive.rbAscii);
            MemoryControlsInfo.Instance.Add(selectEncodingTranceive.rbUnicode);
            MemoryControlsInfo.Instance.Add(selectEncodingReceive.rbHex);
            MemoryControlsInfo.Instance.Add(selectEncodingReceive.rbAscii);
            MemoryControlsInfo.Instance.Add(selectEncodingReceive.rbUnicode);

            MemoryControlsInfo.Instance.Add(tbIp);
            MemoryControlsInfo.Instance.Add(tbPort);
            MemoryControlsInfo.Instance.Add(tbLocalPort);
            MemoryControlsInfo.Instance.Add(cbCom);
            MemoryControlsInfo.Instance.Add(tbBaudRate);
            MemoryControlsInfo.Instance.Add(tbSend1);
            MemoryControlsInfo.Instance.Add(tbSend2);
            MemoryControlsInfo.Instance.Add(tbSend3);
            MemoryControlsInfo.Instance.Add(tbSend4);
            MemoryControlsInfo.Instance.Add(rbCom);
            MemoryControlsInfo.Instance.Add(chbMacros);
            MemoryControlsInfo.Instance.Add(this);

            MemoryControlsInfo.Instance.Load(pathMemory);

        }


        private void selectEncodingTranceive_ChekedChanged(object sender, EventArgs e)
        {
            string s = "";
            if (selectEncodingTranceive.rbHex.Checked)
                s = "Hex";
            else if (selectEncodingTranceive.rbAscii.Checked)
                s = "Ascii";
            else if (selectEncodingTranceive.rbUnicode.Checked)
                s = "Unicode";

            lblHint.Text = string.Format("input cmd ({0})", s);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemoryControlsInfo.Instance.Save(pathMemory);
        }


        void portDataReceivedEventhandler()
        {
            byte[] bytes = port.Read();

            OutputToDisplay(bytes, 0);

            if (chbMacros.Checked && MacrosSelect.Instance.SelectedMacros != null)
                MacrosSelect.Instance.ReceiveMessage(bytes);
        }


        private void cbCom_DropDown(object sender, EventArgs e)
        {
            cbCom.Items.Clear();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                cbCom.Items.Add(s);
            }

        }


        private void chbDtr_CheckedChanged(object sender, EventArgs e)
        {
            if (comport != null)
                comport.DTR = chbDtr.Checked;
        }

        private void chbRts_CheckedChanged(object sender, EventArgs e)
        {
            if (comport != null)
                comport.RTS = chbRts.Checked;
        }

        private void listMacros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMacros.SelectedItems.Count != 0)
            {
                MacrosSelect.Instance.Select(listMacros.SelectedItems[0].Text);
                this.Text = string.Format("Tolkit{0}", " - " + MacrosSelect.Instance.SelectedMacros.GetType().Name);
            }
        }


        void macros_evSendMessage(byte[] bytes)
        {
            Send(bytes);
        }


        #endregion

        bool Send(byte[] bytes)
        {
            try
            {
                if (port.IsOpen)
                {
                    port.Write(bytes);
                    OutputToDisplay(bytes, 1);
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch (Exception ex) { return false; }
            return false;
        }

        byte[] stringToByteArr(string str)
        {
            byte[] bytes = null;
            if (selectEncodingTranceive.rbHex.Checked)
            {
                string[] arr = str.Split(' ');
                bytes = new byte[arr.Length];

                try
                {
                    for (int i = 0; i < arr.Length; i++)
                        bytes[i] = Convert.ToByte(arr[i], 16);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("In the send string meets an invalid character.");
                    return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }

            }

            else if (selectEncodingTranceive.rbAscii.Checked)
            {
                bytes = Encoding.ASCII.GetBytes(str);
            }

            else if (selectEncodingTranceive.rbUnicode.Checked)
            {
                bytes = Encoding.Unicode.GetBytes(str);
            }


            return bytes;
        }

        void OutputToDisplay(byte[] bytes, int flag)
        {
            string NL = "";
            if (MacrosSelect.Instance.IsLineTerminator(bytes))
                NL = "\r\n";

            Invoke(new Action(() =>
                    {
                        switch (flag)
                        {
                            case 0:
                                rchtbReceive.Text += string.Format("{0}{1}", BytesToString(bytes), NL);
                                Autoscroll(rchtbReceive);
                                break;

                            case 1:
                                rchtbTranceive.Text += string.Format("{0}{1}", BytesToString(bytes), NL);
                                Autoscroll(rchtbTranceive);
                                break;
                        }
                    }));
        }

        void Autoscroll(RichTextBox rchtb)
        {
            rchtb.SelectionStart = rchtb.TextLength;
            rchtb.ScrollToCaret();
            rchtb.Refresh();
        }

        private void lbRotate_Click(object sender, EventArgs e)
        {
            string mem = tbPort.Text;
            tbPort.Text = tbLocalPort.Text;
            tbLocalPort.Text = mem;
        }

        private void lbRotate_MouseLeave(object sender, EventArgs e)
        {
            lbRotate.ForeColor = Color.Black;
            lbRotate.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);

        }

        private void lbRotate_MouseMove(object sender, MouseEventArgs e)
        {
            lbRotate.ForeColor = Color.DarkViolet;
            lbRotate.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
        }

        #region Utilites
        string BytesToString(byte[] bytes)
        {
            string str = "";

            if (selectEncodingReceive.rbHex.Checked)
            {
                foreach (byte b in bytes)
                    str += string.Format("0x{0} ", b.ToString("X2"));
            }
            else if (selectEncodingReceive.rbAscii.Checked)
            {
                str = string.Format("{0} ", Encoding.ASCII.GetString((byte[])bytes));
            }

            else if (selectEncodingReceive.rbUnicode.Checked)
            {
                str = string.Format("{0} ", Encoding.Unicode.GetString((byte[])bytes));
            }

            return str;

        }

        #endregion

        private void rbUdp_CheckedChanged(object sender, EventArgs e)
        {
            btDisconnect_Click(null, null);
        }

        private void rbTcp_CheckedChanged(object sender, EventArgs e)
        {
            btDisconnect_Click(null, null);
        }

        private void rbCom_CheckedChanged(object sender, EventArgs e)
        {
            btDisconnect_Click(null, null);
        }

    }
}

