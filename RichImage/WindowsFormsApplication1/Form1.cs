using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RichImage;
using System.Drawing;
using System.Drawing.Imaging;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //imagePanel1.DrawToBitmap
            //imagePanel1.DrawArea_ImageSet()
            //imagePanel1.DrawArea_ImageUpdate()
            Bitmap b = new System.Drawing.Bitmap(4,4, PixelFormat.Format32bppPArgb);
            BitmapData bd = new BitmapData();
            
        }
    }
}
