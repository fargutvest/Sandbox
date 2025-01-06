using System.Drawing;
using System.Drawing.Imaging;

namespace CaptureImage.Common.Extensions
{
    public static class GraphicsExt
    {
        public static void DrawImage(this Graphics graphics, Bitmap bitmap, Rectangle destRect, Rectangle srcRect, float opacity = 1.0f)
        {
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(new ColorMatrix { Matrix33 = opacity }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            graphics.DrawImage(bitmap, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, attributes);
        }
    }
}
