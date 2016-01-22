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
    public partial class MyForm : Form
    {
        BitmapConverter bitmapConverter = new BitmapConverter();
        public MyForm()
        {
            InitializeComponent();
            
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string FilePath = "";
            openFileDialog1.Filter = "Image Files(*.BMP)|*.BMP";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog1.FileName;
                byte[, ,] b = bitmapConverter.BitmapToByteArray(FilePath);
                System.Drawing.Bitmap bmp = bitmapConverter.ByteArrayToBitmap(b);
                bmp.Save(FilePath + ".new.bmp");
            }
        }
    }
}
