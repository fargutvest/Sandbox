using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void a(double Unom)
        {
            char[] U = Unom.ToString().Replace(".", ":").Replace(",", ":").ToCharArray();
            this.Text = "";
            for (int i = 0; i < U.Length; i++)
            {
                this.Text += U[i];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // a(Convert.ToDouble(textBox1.Text));
            //string tt = to_hex_ascii("16384");
            this.Text =calc_bcc(textBox1.Text);
        }

        string to_hex_ascii(string s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            s = "";
            foreach (byte b in bytes) s += b.ToString("X") + " ";
            /*if (s.Length % 2 != 0) s = "0" + s;
            string s_ = null;
            for (int i = 0; i < s.Length; i += 2) { s_ += s.Substring(i, 2) + " "; }
            s_ = s_.Substring(0, s_.Length - 1);
            s = s_;*/
            return s;
        }

        string calc_bcc(string ss)
        {
            ss = ss.Substring(3, ss.Length - 3);
            string[] memArr = ss.Split(new char[] { ' ' });


            int bcc = 0;
            for (int i = 0; i < memArr.Length; i++)
            {
                bcc = bcc + Convert.ToInt32(memArr[i], 16);
            }
            bcc = bcc & 0x7F;
            return bcc.ToString("X");
        }

        
    }
}
