using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace CaptureImage.Common.Helpers
{
    public static class GeometryHelper
    {
        public static GraphicsPath GetGraphicsPath(Rectangle[] rectangles)
        {
            GraphicsPath path = new GraphicsPath();

            if (rectangles.Count() == 0)
                return path;

            Rectangle[] rectanglesOrdered = rectangles.OrderBy(r => r.X).ToArray();

            int offsetX = 0 - rectanglesOrdered.First().X;

            for (int i = 0; i < rectanglesOrdered.Length; i++)
            {
                Rectangle rect = rectanglesOrdered[i];
                rect.Offset(offsetX, 0);

                path.AddRectangle(rect);

            }

            
            return path;
        }
    }
}
