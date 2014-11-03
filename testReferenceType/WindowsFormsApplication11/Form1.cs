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


            while (true)
            {
                aaa aa = new aaa();
                byte[] dd = aa.D;
                methodB(dd);
                byte[] arrr = new byte[3] { 0xff, 0xfe, 0xfc };

                Array.Copy(arrr, dd, 3);
                Adani.MeterrageTime.Instance.meter();
            }



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
    public class aaa
    {
        Random rand;

        byte[] d;

        public aaa()
        {
            rand = new Random();
            d = new byte[3000000];
            rand.NextBytes(d);
        }


        public byte[] D
        {
            get
            {
                byte[] array = new byte[d.Length];
                Array.Copy(d, array, d.Length);
                return array;
            }
        }
    }
}
