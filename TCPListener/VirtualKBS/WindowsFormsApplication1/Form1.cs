using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Adani;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        TcpPortServer server;
        public Form1()
        {
            InitializeComponent();
            server = new TcpPortServer();
            server.Open("192.168.2.133", 56789);
            server.DataReceived += server_DataReceived;
            //server.Message += server_Message;

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (chbAutoclick.Checked)
                        Send(new byte[] { (byte)Keys.A });

                    Thread.Sleep(1000);
                }
            });
        }

        void server_Message(string text)
        {
            Invoke(new Action(() => { this.Text = text; }));
        }

        void server_DataReceived()
        {
            byte[] bytes = server.Read();
            Parser(bytes);
        }

        void Parser(byte[] bytes)
        {
            if (bytes.Length < 2)
                return;
            if (bytes[0] != 0x0)
                return;

            switch (bytes[1])
            {
                case 0xbb: //смена ip
                    server.Write(bytes);
                    break;

                //светодиоды
                case 0xa0: //выключен A
                    Invoke(new Action(() => { pbLedA.BackColor = SystemColors.Control; }));
                    break;
                case 0xa1: //включен A
                    Invoke(new Action(() => { pbLedA.BackColor = Color.Green; }));
                    break;
                case 0xb0: //выключен B
                    Invoke(new Action(() => { pbLedB.BackColor = SystemColors.Control; }));
                    break;
                case 0xb1: //включен B
                    Invoke(new Action(() => { pbLedB.BackColor = Color.Green; }));
                    break;
                case 0xc0: //выключен C
                    Invoke(new Action(() => { pbLedC.BackColor = SystemColors.Control; }));
                    break;
                case 0xc1: //включен C
                    Invoke(new Action(() => { pbLedC.BackColor = Color.Green; }));
                    break;
                case 0xd0: //выключен D
                    Invoke(new Action(() => { pbLedD.BackColor = SystemColors.Control; }));
                    break;
                case 0xd1: //включен D
                    Invoke(new Action(() => { pbLedD.BackColor = Color.Green; }));
                    break;
                case 0xe0: //выключен E
                    Invoke(new Action(() => { pbLedE.BackColor = SystemColors.Control; }));
                    break;
                case 0xe1: //включен E
                    Invoke(new Action(() => { pbLedE.BackColor = Color.Green; }));
                    break;
            }

        }

        private void btPost_Click(object sender, EventArgs e)
        {
            server.Write(Encoding.ASCII.GetBytes(tbInput.Text));
        }

        #region keys keyboard

        private void btA_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.A });
        }

        private void btB_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.B });
        }

        private void btC_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.C });
        }

        private void btD_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D });
        }

        private void btMenu_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Oemtilde });
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Up });
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Down });
        }

        private void btE_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.E });
        }

        private void btF_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.F });
        }

        private void btG_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.G });
        }

        private void btH_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.H });
        }

        private void btI_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.I });
        }

        private void btJ_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.J });
        }

        private void btBackspace_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Back });
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Delete });
        }

        private void btEsc_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Escape });
        }

        private void btEnter_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Enter });
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Right });
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Left });
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D1 });
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D2 });
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D3 });
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D4 });
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D5 });
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D6 });
        }

        private void bt7_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D7 });
        }

        private void bt8_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D8 });
        }

        private void bt9_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D9 });
        }

        private void bt0_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.D0 });
        }

        private void btPlus_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Add });
        }

        private void btMinus_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Subtract });
        }

        private void btTab_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.Tab });
        }

        private void btF1_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.F1 });
        }

        private void btF2_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.F2 });
        }

        private void btF3_Click(object sender, EventArgs e)
        {
            Send(new byte[] { (byte)Keys.F3 });
        }

        void Send(byte[] keycode)
        {
            byte[] bytes = new byte[4];
            bytes[0] = 0x00;
            bytes[1] = 0x11;
            bytes[2] = 0x00;
            bytes[3] = keycode[0];
            //отправка нажатия
            server.Write(bytes);
            bytes[1] = 0x22;
            //отправка отжатия
            server.Write(bytes);
        }

        #endregion
    }
}
