using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Common.Helpers
{
    public static class GraphicsHelper
    {
        public static void DrawBorder(Graphics g, Rectangle r)
        {
            var d = 4;
            //r.Inflate(d, d);
            r.Offset(d, d);
            r.Width -= d*2;
            r.Height -= d*2;

            ControlPaint.DrawBorder(g, r, Color.Black, ButtonBorderStyle.Dotted);
            var rectangles = new List<Rectangle>();
            var r1 = new Rectangle(r.Left - d, r.Top - d, (int)(d), (int)(d)); rectangles.Add(r1);
            r1.Offset(r.Width / 2, 0); rectangles.Add(r1);
            r1.Offset(r.Width / 2, 0); rectangles.Add(r1);
            r1.Offset(0, r.Height / 2); rectangles.Add(r1);
            r1.Offset(0, r.Height / 2); rectangles.Add(r1);
            r1.Offset(-r.Width / 2, 0); rectangles.Add(r1);
            r1.Offset(-r.Width / 2, 0); rectangles.Add(r1);
            r1.Offset(0, -r.Height / 2); rectangles.Add(r1);
            g.FillRectangles(Brushes.Black, rectangles.ToArray());
            g.DrawRectangles(Pens.White, rectangles.ToArray());
        }
    }
}
