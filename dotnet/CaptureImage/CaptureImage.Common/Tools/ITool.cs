using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Common.Tools
{
    public interface ITool
    {
        void Activate();
        void Deactivate();

        void MouseHoverControl(Point mouse);

        void MouseMove(Graphics gr, Point mouse);

        void MouseMove(Control control, Point mouse);

        void MouseUp();

        void MouseDown(Point mouse, bool onControl = false);
    }
}
