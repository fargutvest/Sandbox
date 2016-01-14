using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace WindowsFormsApplication1
{
    public class BitmapConverter
    {
        public byte[, ,] BitmapToByteArray(string patch)
        {
            byte[, ,] bOut = null;
            Bitmap bmp = new Bitmap(patch);
            bOut = convert0(bmp);
            //bOut = convertNaiv(bmp);
            return bOut;
        }
        public Bitmap ByteArrayToBitmap(byte[, ,] b)
        {
            Bitmap bmpOut = null;
            bmpOut = convert(b);
            return bmpOut;
        }


        byte[, ,] convertNaiv(Bitmap bmp)
        {
            int width = bmp.Width,
                 height = bmp.Height;
            byte[, ,] res = new byte[4, height, width];
            Color c;

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    c = bmp.GetPixel(w, h);
                    res[0, h, w] = c.A;
                    res[1, h, w] = c.R;
                    res[2, h, w] = c.G;
                    res[3, h, w] = c.B;
                }
            }

            return res;
        }


        unsafe byte[, ,] convert0(Bitmap bmp)
        {
            int width = bmp.Width,
                height = bmp.Height;
            byte[, ,] res = new byte[3, height, width];
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            try
            {
                byte* curpos; //current position

                for (int h = 0; h < height; h++)
                {
                    curpos = ((byte*)bd.Scan0) + h * bd.Stride;
                    for (int w = 0; w < width; w++)
                    {
                        res[2, h, w] = *(curpos++);
                        res[1, h, w] = *(curpos++);
                        res[0, h, w] = *(curpos++);
                    }
                }

            }
            finally
            {
                bmp.UnlockBits(bd);
            }
            return res;
        }

        unsafe byte[, ,] convert1(Bitmap bmp)
        {
            int width = bmp.Width,
                height = bmp.Height;
            byte[, ,] res = new byte[3, height, width];
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            try
            {
                byte* curpos; //current position
                fixed (byte* _res = res)
                {
                    byte* _r = _res , _g = _res + 1, _b = _res + 2;
                    for (int h = 0; h < height; h++)
                    {
                        curpos = ((byte*)bd.Scan0) + h * bd.Stride;
                        for (int w = 0; w < width; w++)
                        {
                            *_b = *(curpos++);
                            _b += res.GetLength(0);
                            *_g = *(curpos++);
                            _g += res.GetLength(0);
                            *_r = *(curpos++);
                            _r += res.GetLength(0);
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bd);
            }
            return res;
        }

        Bitmap convert(byte[, ,] b)
        {

            int ch = b.GetLength(0),
             height = b.GetLength(1),
             width = b.GetLength(2);
            Bitmap bmpOut = new Bitmap(width, height);
            Color c;
            //b[ch,height,width]

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    c = Color.FromArgb(b[0, h, w], b[1, h, w], b[2, h, w]); //без альфа канала
                    bmpOut.SetPixel(w, h, c);
                }
            }

            return bmpOut;
        }
    }
}
