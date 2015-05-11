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
        public Form1()
        {
            InitializeComponent();
            test();
        }

        void test()
        {
            Random rand = new Random();
            Bitmap bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    int grey = rand.Next(255);
                    bitmap.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }

            pictureBox1.Image = bitmap;
            //Graphics graphics = Graphics.FromImage(bitmap);


        }
    }
}
