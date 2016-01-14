using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace test
{
    public class TaskImageGenerate
    {

        Task task;
        CancellationTokenSource cts;
        public event EventHandler<Bitmap> ImageLineEvent;
        Graphics graphics;
        Bitmap bitmap;
        int x = 0; // координата ширины
        int height=0;
        int width = 0;
        byte[] IMGreal;
        public TaskImageGenerate(int _height, int _width)
        {
            //Image img = Image.FromFile(@"D:\1.bmp");
            //IMGreal = ImageToByte(img);
            width = _width;
            height = _height;
            bitmap = new Bitmap(_width, _height);
            graphics = Graphics.FromImage(bitmap);
            cts = new CancellationTokenSource();
            task = new Task(Execute, cts.Token);
            task.Start();
        }

        void Execute(object _ct)
        {
            CancellationToken ct = (CancellationToken)_ct;
            Random r = new Random();
            while (true)
            {
                byte [] b = GenerateIMG(height);
                //byte[] b = new byte[height];
                //Array.Copy(IMGreal,(x+1)*height,b,0,height);
                Bitmap buffBitmap = new Bitmap(1,height);
                for (int i = 0; i < height; i++)
                {
                    int gray = b[i]; 
                    Color RGB = Color.FromArgb(gray, gray, gray);
                    buffBitmap.SetPixel(0, i, RGB);

                }
                smartIncX();
                graphics.DrawImage(buffBitmap, x, 0);
                ct.ThrowIfCancellationRequested();
                Thread.Sleep(0);
                if (ImageLineEvent != null)
                {
                    ImageLineEvent(this,bitmap);
                }
            }

        }

        byte[] GenerateIMG(int len)
        {
            byte[] bOut = new byte[len];
            Random rand = new Random();
            for (int i = 0; i < bOut.Length; i++)
            {
                bOut[i] = Convert.ToByte(rand.Next(0, 255));
            }
            return bOut;
        }

        void smartIncX()
        {
            x++;
            if (x == width) { x = 0; }
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        

    }
}
