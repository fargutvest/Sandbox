using System.Windows.Forms;
using System.Drawing;

namespace CaptureImage.Common.Extensions
{
    public static class ControlExt
    {
        public static Point GetMousePosition(this Control control) => control.PointToClient(Control.MousePosition);

    }
}
