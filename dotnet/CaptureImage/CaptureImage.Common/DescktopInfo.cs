
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CaptureImage.Common
{
    public class DescktopInfo
    {
        public ScreenInfo[] ScreenInfos { get; set; }

        public Bitmap Background { get; set; }

        public GraphicsPath Path { get; set; } 

        public Point Location { get; set; }
        public Size ClientSize { get; set; }
        
    }
}
