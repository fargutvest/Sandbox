using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Common.Tools
{
    public class LineTool : ITool
    {
        private DrawingState state;
        private Point mouseStartPos;
        private Point mousePreviousControlPos;
        private Pen pen;
        private bool isActive;

        public LineTool()
        {
            this.state = DrawingState.None;

            mouseStartPos = new Point(0, 0);
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
                    mouseStartPos = mousePosition;

                state = DrawingState.Drawing;
            }
        }

        public void MouseHoverControl(Point mousePositionOnControl)
        {

        }

        public void MouseMove(Graphics gr, Point mouse)
        {
            if (isActive)
            {
                if (state == DrawingState.Drawing)
                {
                    gr.DrawLine(pen, mouseStartPos, mouse);
                }
            }
        }

        public void MouseMove(Control canvas, Point mouse)
        {
            if (isActive)
            {
                if (state == DrawingState.Drawing)
                {
                    canvas.CreateGraphics().DrawLine(pen, mousePreviousControlPos, mouse);
                }
            }
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
