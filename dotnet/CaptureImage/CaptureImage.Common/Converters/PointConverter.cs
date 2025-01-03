using System.Drawing;

namespace CaptureImage.Common.Converters
{
    public static class PointConverter
    {
        public static System.Windows.Point Convert(this PointF point) => new System.Windows.Point(point.X, point.Y); 
 
    }
}
