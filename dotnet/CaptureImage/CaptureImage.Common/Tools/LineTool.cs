using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Common.Tools
{
    public class LineTool : ITool
    {
        private DrawingState state;
        private Point mousePreviousPos;
        private Point mousePreviousControlPos;
        private Pen pen;
        private bool isActive;

        public LineTool()
        {
            this.state = DrawingState.None;

            mousePreviousPos = new Point(0, 0);
            pen = new Pen(Color.Yellow)
            {
                Width = 2,
            };
        }

        public void MouseDown(Point mousePosition, bool onControl = false)
        {
            if (isActive)
            {
                if (onControl)
                    mousePreviousControlPos = mousePosition;
                else
                    mousePreviousPos = mousePosition;

                state = DrawingState.Drawing;
            }
        }

        public void MouseHoverControl(Point mousePositionOnControl)
        {
            if (isActive)
            {
                mousePreviousControlPos = mousePositionOnControl;
            }
        }

        public void MouseMove(Graphics gr, Point mouse)
        {
            
        }

        public void MouseMove(Control control, Point mouse)
        {
            
        }

        public void MouseUp()
        {
            if (isActive)
            {
                state = DrawingState.None;
            }
        }

        public void Activate()
        {
            isActive = true;
        }

        public void Deactivate()
        {
            isActive = false;
        }
    }
}
