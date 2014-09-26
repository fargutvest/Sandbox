using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication11
{
    public partial class Form1 : Form
    {
        string[] a = new string[] { "a", "b", "c" };
        string[] b = new string[] { "d", "e", "f" };
        public Form1()
        {
            InitializeComponent();
            a = b;
            b[1] = "x";
            this.Text = a[0] + a[1] + a[2];


            string[] c = new string[] { "a", "b", "c" };
            

            

            methodA(c);

            byte[] d = new byte[] {0x0, 0x1, 0x2, 0x3 };
            byte[] dd = d;
            methodB(dd);


            byte bb = 0x05;
            methodC(bb);



            String str = "qwertyui";

            methodE(str);
            
            



        }

        void methodA(string[] strArr)
        {

            strArr[1] = "y";
        }

        void methodB(byte[] byteArr)
        {
            byteArr[1] = 0xff;
        }


        void methodC(byte b)
        {
            b = 0xff;
        }

        void methodE(string st)
        {
            st = "zxcvb";

        }


    }
}
