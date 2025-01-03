using CaptureImage.Common.Converters;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Media;

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

        public static PathGeometry GetPathGeometry(GraphicsPath path)
        {
            PathGeometry polygonGeometry = new PathGeometry();
            List<PathSegment> segments = new List<PathSegment>();

            PointF[] points = path.PathPoints.Distinct().ToArray();

            for (int i = 0; i < path.PathPoints.Length; i++)
            {
                PointF pointF = path.PathPoints[i];
              
                LineSegment segment = new LineSegment(pointF.Convert(), true);
                segments.Add(segment);

                // если дошли до 4го угла экрана
                if ((i + 1) %4 == 0)
                {
                    // возвращаемся в первый угол экрана
                    PointF topLeftPoint = path.PathPoints[i - 3];

                    LineSegment topLeftSegment = new LineSegment(topLeftPoint.Convert(), true);
                    segments.Add(topLeftSegment);
                }    
            }
            PointF startPoint = path.PathPoints[0];

            PathFigure figure = new PathFigure(startPoint.Convert(), segments.ToArray(), true);
            polygonGeometry.Figures.Add(figure);
            return polygonGeometry;
        }
    }
}
