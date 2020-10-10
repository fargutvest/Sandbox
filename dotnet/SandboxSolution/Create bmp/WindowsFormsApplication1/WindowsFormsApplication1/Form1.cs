using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            read();
            create();
        }

        void read()
        {
            Bitmap bmp = new Bitmap(@"C:\1.bmp");
            Color c = bmp.GetPixel(0, 0);
        }

        void create()
        {
            Bitmap bmp = new Bitmap(5,5);
            bmp.SetPixel(0, 0, Color.Red);
            bmp.SetPixel(0, 4, Color.Green);
            bmp.SetPixel(4, 0, Color.Blue);
            bmp.SetPixel(4, 4, Color.Black);
            bmp.SetPixel(2, 2, Color.Yellow);
            bmp.Save(@"C:\save.bmp");
        }
    }
}
