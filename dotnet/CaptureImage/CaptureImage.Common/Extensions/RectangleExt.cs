using System.Drawing;

namespace CaptureImage.Common.Extensions
{
    public static class RectangleExt
    {
        public static Rectangle Clone(this Rectangle rect) => new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
    }
}
