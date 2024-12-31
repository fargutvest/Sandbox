using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Helpers
{
    internal static class CaptureImageHelper
    {
        internal static Bitmap CaptureMyScreen(Rectangle rect)
        {
            try
            {
                Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(new Point(rect.X, rect.Y), Point.Empty, rect.Size);
                    return bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
