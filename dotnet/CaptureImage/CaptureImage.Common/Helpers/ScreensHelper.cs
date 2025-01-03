using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace CaptureImage.Common.Helpers
{
    public static class ScreensHelper
    {
        public static ScreenInfo[] GetScreenInfos()
        {
            Screen[] screens = Screen.AllScreens.ToArray();

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

        public static DescktopInfo GetDesctopInfo()
        {
            ScreenInfo[] infos = ScreensHelper.GetScreenInfos();

            Bitmap background = BitmapHelper.Glue(infos.Select(t => t.Screenshot).ToArray());

            GraphicsPath path = GeometryHelper.GetGraphicsPath(infos.Select(t => t.Screen.Bounds).ToArray());
            int minX = infos.Min(t => t.Screen.Bounds.X);
            int minY = infos.Min(t => t.Screen.Bounds.Y);
            Point location = new Point(minX, minY);

            int maxX = (int)path.PathData.Points.Max(p => p.X);
            int maxY = (int)path.PathData.Points.Max(p => p.Y);
            Size clientSize = new Size(maxX, maxY);

            DescktopInfo descktopInfo = new DescktopInfo()
            {
                ScreenInfos = infos,
                Background = background,
                Path = path,
                Location = location,
                ClientSize = clientSize
            };

            return descktopInfo;


        }
    }
}
