using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CaptureImage.Helpers;

namespace CaptureImage
{
    internal class App
    {
        internal void Run()
        {
            Screen[] screens = Screen.AllScreens.ToArray();

            Tuple<Screen, Bitmap>[] tuples = new Tuple<Screen, Bitmap>[screens.Length];

            for (int i = 0; i < screens.Length; i++)
            {
                Screen screen = screens[i];
                Rectangle bounds = screen.Bounds;
                Bitmap bitmap = CaptureImageHelper.CaptureMyScreen(bounds);
                tuples[i] = new Tuple<Screen, Bitmap>(screen, bitmap);
            }

            Tuple<Screen, Bitmap>[] orderedTuples = tuples.OrderBy(t => t.Item1.Bounds.X).ToArray();

            Bitmap background = BitmapHelper.Glue(orderedTuples.Select(t => t.Item2).ToArray());

            GraphicsPath path = GeometryHelper.GetGraphicsPath(tuples.Select(t => t.Item1.Bounds).ToArray());
            int minX = tuples.Min(t => t.Item1.Bounds.X);
            int minY = tuples.Min(t => t.Item1.Bounds.Y);
            Point location = new Point(minX, minY);

            int maxX = (int)path.PathData.Points.Max(p => p.X);
            int maxY = (int)path.PathData.Points.Max(p => p.Y);
            Size clientSize = new Size(maxX, maxY);

            FreezeScreen freezeScreen = new FreezeScreen(clientSize, location)
            {
                BackgroundImage = background
            };

            freezeScreen.Show();

            BlackoutScreen blackoutScreen = new BlackoutScreen(clientSize, location)
            {
                BackColor = Color.Black,
                BackgroundImage = BitmapHelper.ChangeOpacity(background, 0.5f),
                TransparencyKey = Color.Red,
                //TopMost = true 
            };
            blackoutScreen.Show();

        }
    }
}
