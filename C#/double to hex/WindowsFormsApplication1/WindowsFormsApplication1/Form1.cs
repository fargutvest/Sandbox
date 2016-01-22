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
            string r = 16384.ToString("X");
            double_to_hex(15);
           string alo = calc_bcc("01 52 31 02 44 41 54 45 5f 28 29 03");
            
        }

        string double_to_hex(int a)
        {
            string s = a.ToString("X");
            if (s.Length % 2 != 0) s = "0" + s;
            string s_ = null;
            for (int i = 0; i < s.Length; i += 2) { s_ += s.Substring(i, 2)+" "; }
            s_ = s_.Substring(0,s_.Length-1);
            s = s_;
            return s;
        }
        string calc_bcc(string s)
        {
            s = s.Substring(3, s.Length - 3);
            string[] memArr = s.Split(new char[] { ' ' });


            int bcc = 0;
            for (int i = 0; i < memArr.Length; i++)
            {
                bcc = bcc + Convert.ToInt32(memArr[i], 16);
            }
            bcc = bcc & 0xFF;
            return bcc.ToString("X");
        }

        void b(List<object> tt)
        {
            int w = (int)tt[0];
            int ee = (int)tt[1];
        }

        void b(List<object> tt)
        {

        }
        void b(List<object> tt)
        {

        }

    }
}
