using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CaptureImage.Common.Helpers
{
    public static class BitmapHelper
    {
        /// <summary>
        /// https://chatgpt.com/
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Bitmap DarkenImage(Bitmap bitmap, float factor)
        {
            // Получаем информацию о изображении
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            // Блокируем изображение в памяти для быстрого доступа
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // Получаем указатель на данные изображения
            IntPtr ptr = bitmapData.Scan0;

            // Количество байтов, которые занимают пиксели
            int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;

            // Создаем массив для хранения данных пикселей
            byte[] rgbValues = new byte[bytes];

            // Копируем данные изображения в массив
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Проходим по всем пикселям
            for (int i = 0; i < rgbValues.Length; i += 4) // 4 байта на пиксель (для формата ARGB)
            {
                // Получаем текущие значения цвета (A, R, G, B)
                byte b = rgbValues[i];     // синий
                byte g = rgbValues[i + 1]; // зеленый
                byte r = rgbValues[i + 2]; // красный

                // Применяем коэффициент затемнения
                r = (byte)(r * factor);
                g = (byte)(g * factor);
                b = (byte)(b * factor);

                // Записываем измененные значения обратно в массив
                rgbValues[i] = b;
                rgbValues[i + 1] = g;
                rgbValues[i + 2] = r;
            }

            // Копируем измененные данные обратно в изображение
            Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Разблокируем изображение
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/14364716/faster-algorithm-to-change-hue-saturation-lightness-in-a-bitmap
        /// </summary>
        /// <param name="original"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Bitmap DarkenImage2(Bitmap original, float factor)
        {
            float rwgt = 0.3086f;
            float gwgt = 0.6094f;
            float bwgt = 0.0820f;

            float saturation = 1.0f;
            float brightness = 0.9f;

            ColorMatrix colorMatrix = new ColorMatrix();
            ImageAttributes imageAttributes = new ImageAttributes();


            float baseSat = 1.0f - saturation;

            colorMatrix[0, 0] = baseSat * rwgt + saturation;
            colorMatrix[0, 1] = baseSat * rwgt;
            colorMatrix[0, 2] = baseSat * rwgt;
            colorMatrix[1, 0] = baseSat * gwgt;
            colorMatrix[1, 1] = baseSat * gwgt + saturation;
            colorMatrix[1, 2] = baseSat * gwgt;
            colorMatrix[2, 0] = baseSat * bwgt;
            colorMatrix[2, 1] = baseSat * bwgt;
            colorMatrix[2, 2] = baseSat * bwgt + saturation;

            float adjustedBrightness = brightness - 1f;

            colorMatrix[4, 0] = adjustedBrightness;
            colorMatrix[4, 1] = adjustedBrightness;
            colorMatrix[4, 2] = adjustedBrightness;

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


            Bitmap darkenedImage = new Bitmap(original.Width, original.Height);

            Graphics gr = Graphics.FromImage(darkenedImage);

            gr.DrawImage(original, new Rectangle(0,0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, imageAttributes);

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


