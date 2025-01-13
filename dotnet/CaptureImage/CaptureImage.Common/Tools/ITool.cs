using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Common.Tools
{
    public interface ITool
    {
        void Activate();

        void Deactivate();

        void MouseMove(Point mouse);

        void MouseUp();

        void MouseDown(Point mouse);
    }
}
