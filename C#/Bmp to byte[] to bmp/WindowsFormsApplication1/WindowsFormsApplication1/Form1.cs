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
        string pathFile = "";
        BitmapConverter bitmapConverter = new BitmapConverter();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.BMP)|*.BMP";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pathFile = openFileDialog1.FileName;
                lblPath.Text = pathFile;
            }
        }

        private void btnToBMP_Click(object sender, EventArgs e)
        {
            bitmapConverter.ByteArrayToBitmap(new byte[] { 0 });
        }

        private void btnToArrayByte_Click(object sender, EventArgs e)
        {
            bitmapConverter.BitmapToByteArray(pathFile);
        }
    }
}
