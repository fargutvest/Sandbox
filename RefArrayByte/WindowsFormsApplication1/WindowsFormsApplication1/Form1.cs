using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Byte[] b;
        string s;
        Wrapping w = new Wrapping();

        public Form1()
        {
            InitializeComponent();
            b = new Byte[] { 0, 1, 2, 3 };
            classA _classA = new classA(b);
            Byte[] bbb = b;


            s = "поляна";
            _classA = new classA(s);
            string sss = s;

            w.b = new byte[] { 25, 26, 27, 28 };
            _classA = new classA(w);
            Wrapping wwww = new Wrapping();
            wwww.b = w.b;

        }
    }

    public class classA
    {
        Byte[] refb;
        string refs;
        Wrapping refw = new Wrapping();



        public classA(Byte[] _b)
        {
            refb = _b;
            refb = new Byte[] { 5,6,7,8 };
        }

        public classA(string _s)
        {
            refs = _s;
            refs = "смородина";
        }

        public classA(Wrapping _w)
        {
            refw = _w;
            refw.b = new byte[] { 11, 12, 13, 14 };
        }

    }

    public class Wrapping //класс обертка
    {
        public byte[] b;
    }
}
