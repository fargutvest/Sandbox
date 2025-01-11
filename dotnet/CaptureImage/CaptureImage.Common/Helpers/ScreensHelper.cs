using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace CaptureImage.Common.Helpers
{
    public static class ScreensHelper
    {
        public static ScreenInfo[] GetScreenInfos()
        {
            Screen[] screens = Screen.AllScreens.Take(2).ToArray();

            ScreenInfo[] infos = new ScreenInfo[screens.Length];

            for (int i = 0; i < screens.Length; i++)
            {
                Screen screen = screens[i];
                Rectangle bounds = screen.Bounds;
                Bitmap bitmap = CaptureImageHelper.CaptureMyScreen(bounds);
                infos[i] = new ScreenInfo() 
                {
                    Screen = screen,
                    Screenshot = bitmap
                };
            }

            ScreenInfo[] orderedInfos = infos.OrderBy(t => t.Screen.Bounds.X).ToArray();

            return orderedInfos;
        }

        public static DescktopInfo GetDesktopInfo()
        {
            ScreenInfo[] infos = GetScreenInfos();

            Bitmap background = BitmapHelper.Glue(infos.Select(t => t.Screenshot).ToArray());

            GraphicsPath path = GeometryHelper.GetGraphicsPath(infos.Select(t => t.Screen.Bounds).ToArray());
            int minX = infos.Min(t => t.Screen.Bounds.X);
            int minY = infos.Min(t => t.Screen.Bounds.Y);
            Point location = new Point(minX, minY);

            int maxX = (int)path.PathData.Points.Max(p => p.X);
            int maxY = (int)path.PathData.Points.Max(p => p.Y);
            Size clientSize = new Size(maxX, maxY);

            PathGeometry polygonGeometry = GeometryHelper.GetPathGeometry(path);

            DescktopInfo desktopInfo = new DescktopInfo()
            {
                ScreenInfos = infos,
                Background = background,
                BackgroundRect = new Rectangle(0, 0, maxX, maxY),
                Path = path,
                PolygonGeometry = polygonGeometry,
                Location = location,
                ClientSize = clientSize,
                Bounds = new Rectangle(minX, minY, maxX, maxY)
            };

            return desktopInfo;


        }
    }
}
