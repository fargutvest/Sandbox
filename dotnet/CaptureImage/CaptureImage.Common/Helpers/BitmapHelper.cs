using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace CaptureImage.Common.Helpers
{
    public static class BitmapHelper
    {
        public static Bitmap DarkenImage(Bitmap original, float factor)
        {
            // Создаем новое изображение с теми же размерами, что и исходное
            Bitmap darkenedImage = new Bitmap(original.Width, original.Height);

            // Проходим по каждому пикселю изображения
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    // Получаем текущий цвет пикселя
                    Color pixelColor = original.GetPixel(x, y);

                    // Уменьшаем яркость пикселя
                    int r = (int)(pixelColor.R * factor);
                    int g = (int)(pixelColor.G * factor);
                    int b = (int)(pixelColor.B * factor);

                    // Ограничиваем значения до 255
                    r = Math.Min(255, r);
                    g = Math.Min(255, g);
                    b = Math.Min(255, b);

                    // Создаем новый цвет с уменьшенной яркостью
                    Color newColor = Color.FromArgb(r, g, b);

                    // Устанавливаем новый цвет пикселя на изображении
                    darkenedImage.SetPixel(x, y, newColor);
                }
            }

            return darkenedImage;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/4779027/changing-the-opacity-of-a-bitmap-image
        /// </summary>
        public static Bitmap ChangeOpacity(Bitmap image, float opacity)
        {
            try
            {
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = opacity;
                    ImageAttributes attributes = new ImageAttributes();
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    gr.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static Bitmap Glue(Bitmap[] bitmaps)
        {
            Bitmap output = new Bitmap(bitmaps.Sum(b => b.Width), bitmaps.Max(b => b.Height));

            using (Graphics gr = Graphics.FromImage(output))
            {
                int offsetX = 0;
                int offsetY = 0;

                for (int i = 0; i < bitmaps.Length; i++)
                {
                    Bitmap bmp = bitmaps[i];
                    gr.DrawImage(bmp, offsetX, offsetY);
                    offsetX += bmp.Width;
                }
            }

            return output;
        }

        public static Bitmap Crop(Bitmap source, Rectangle section)
        {
            Bitmap bmpImage = new Bitmap(source);
            return bmpImage.Clone(section, bmpImage.PixelFormat);
        }
    }
}


