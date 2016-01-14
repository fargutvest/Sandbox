using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Size = new Size(Screen.PrimaryScreen.Bounds.Width / _scale,
                Screen.PrimaryScreen.Bounds.Height / _scale);
            Begin();
        }

        CancellationTokenSource _cts = new CancellationTokenSource();
        Task _task;
        int _scale = 16;

        private void Begin()
        {
            _task = Task.Factory.StartNew(() =>
               {
                   while (true)
                   {
                       pictureBox1.Invoke(new Action(() =>
                       {
                           WriteableBitmap wb = new WriteableBitmap(pictureBox1.Size.Width,
                               pictureBox1.Size.Height, 96, 96, PixelFormats.Bgr24, null);
                           byte[] array = new byte[pictureBox1.Size.Width *
                               pictureBox1.Size.Height *
                               PixelFormats.Bgr24.BitsPerPixel / 8];
                           new Random().NextBytes(array);
                           int stride = pictureBox1.Size.Width *
                               PixelFormats.Bgr24.BitsPerPixel / 8;

                           wb.WritePixels(new System.Windows.Int32Rect(0, 0,
                               pictureBox1.Size.Width,
                               pictureBox1.Size.Height), array, stride, 0);
                           pictureBox1.Image = BitmapFromWriteableBitmap(wb);
                           wb = null;
                           GC.Collect();
                           


                       }));
                       Task.Delay(1).Wait();

                       if (_cts.Token.IsCancellationRequested)
                           return;
                   }
               });
        }

        private System.Drawing.Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeBmp)
        {
            System.Drawing.Bitmap bmp;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
                enc.Save(outStream);
                bmp = new System.Drawing.Bitmap(outStream);
            }
            return bmp;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts.Cancel();
        }

    }
}
