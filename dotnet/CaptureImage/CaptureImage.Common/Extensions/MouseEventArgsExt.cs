using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Common.Extensions
{
    public static class MouseEventArgsExt
    {
        public static MouseEventArgs Offset(this MouseEventArgs e, Point offset)
        {
            return new MouseEventArgs(e.Button, e.Clicks, e.X + offset.X, e.Y + offset.Y, e.Delta);
        }
    }
}
