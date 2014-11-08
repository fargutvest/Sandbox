using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           

            

       
            /*System.Drawing.Graphics g = pictureBox1.CreateGraphics();
            
            g.DrawLine(p, 0, 0, 100, 0);
            */

           /* Image image = Image.FromFile("C:\\pict.bmp");
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.bmp);
            byte[] b = memoryStream.ToArray();



            System.IO.MemoryStream memoryStream1 = new System.IO.MemoryStream();
            foreach (byte b1 in b) memoryStream1.WriteByte(b1);
            Image image1 = Image.FromStream(memoryStream1);
            image1.Save("C:\\pict1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);*/


            /*byte[] b = new byte[]{0x24,0x56,0x32,0x54,0x68,0x95,0x87,0x54,0x33};

            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap _bitmap1 = (Bitmap)tc.ConvertFrom(b);
            */
        }

        void buildBMP(byte[] byteIMG)
        {
            //http://c-site.h1.ru/infa/bmp_struct.htm
            //заголовок файла
            byte[] BM = new byte[2] {0x42,0x4d};
            byte[] SizeFile = new byte[4]; 
            byte[] res1 = new byte[2];
            byte[] res2 = new byte[2];
            byte[] offset = new byte[4];

            //Заголовок BITMAP (Информация об изображении)
            byte[] SizeHeader = new byte[4];
            byte[] WeightImage = new byte[4];
            byte[] HeightImage = new byte[4];
            byte[] PlaneCount = new byte[2];
            byte[] BitOfPixel = new byte[2];
            byte[] CompressionType = new byte[4];
            byte[] SizeCompressionImage = new byte[4];
            byte[] HorizontalResolution = new byte[4];
            byte[] VerticalResolution = new byte[4];
            byte[] CountUseColor = new byte[4];
            byte[] CountImportantColor = new byte[4];

            int intSizeFile = 54+byteIMG.Length;

            SizeFile = new byte[4];

            System.Drawing.Bitmap b = new Bitmap(100, 100);
           


            /* foreach (byte b1 in b) memoryStream1.WriteByte(b1);
            Image image1 = Image.FromStream(memoryStream1);
            image1.Save("C:\\pict1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);*/


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics _graphics = panel1.CreateGraphics();
            Bitmap _bitmap = new Bitmap(panel1.Width, 100);
            Random rnd = new Random();
            byte[] R = new byte[100];
            byte[] G = new byte[100];
            byte[] B = new byte[100];



            _bitmap.SetPixel(10, 10, Color.Red);
            _graphics.DrawImage((Image)_bitmap, 0, 0);

            /*
            for (int x = 0; x < panel1.Width; x++)
            {
                rnd.NextBytes(R);
                rnd.NextBytes(G);
                rnd.NextBytes(B);
                for (int y = 0; y < 100; y++)
                {
                    _bitmap.SetPixel(x, y, Color.FromArgb(R[y], G[y], B[y]));
                    _graphics.DrawImage((Image)_bitmap, 0, 0);
                }
                
                
            }*/
           


        }
    }
}
