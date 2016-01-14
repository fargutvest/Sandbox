using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using STROKESCRIBECLSLib;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StrokeScribeClass ss = new StrokeScribeClass();
            ss.Alphabet = enumAlphabet.CODE128B;
            ss.Text = textBox1.Text ;
            ss.FontColor = 0x000000;
            int width = ss.BitmapW;
            int rc = ss.SavePicture("bardcode.bmp", enumFormats.BMP, width * 2, width); 
          
            

            //загрузим картинку
            System.IO.FileStream fs = new System.IO.FileStream("bardcode.bmp" , System.IO.FileMode.Open);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
            fs.Close();
            pictureBox1.Image = img;

        }
    }
}
