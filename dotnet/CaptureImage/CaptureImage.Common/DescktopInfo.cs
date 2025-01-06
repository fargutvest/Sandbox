
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Media;

namespace CaptureImage.Common
{
    public class DescktopInfo
    {
        public ScreenInfo[] ScreenInfos { get; set; }

        public Bitmap Background { get; set; }
        public Rectangle BackgroundRect { get; set; }

        public GraphicsPath Path { get; set; } 

        public PathGeometry PolygonGeometry { get; set; }

        public Rectangle Bounds { get; set; }

        public Point Location { get; set; }
        public Size ClientSize { get; set; }
        
    }
}
