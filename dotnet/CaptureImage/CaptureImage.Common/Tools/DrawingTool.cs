using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Common.Tools
{
    public class DrawingTool
    {
        private DrawingState state;

        private Point mousePreviousPos;

        public Point? mousePreviousControlPos;

        private Pen pen;

        private bool isActive;

        public DrawingTool(bool isActive)
        {
            this.isActive = isActive;

            this.state = DrawingState.None;

            mousePreviousPos = new Point(0, 0);
            pen = new Pen(Color.Yellow)
            {
                Width = 2,
            };
        }

        public void MouseMove(Graphics gr, Point mouse)
        {
            if (isActive)
            {
                if (state == DrawingState.Drawing)
                {
                    gr.DrawLine(pen, mousePreviousPos, mouse);
                    mousePreviousPos = mouse;
                }
            }
        }

        public void MouseMove(Control canvas, Point mouse)
        {
            if (isActive)
            {
                if (state == DrawingState.Drawing)
                {
                    if (mousePreviousControlPos.HasValue)
                        canvas.CreateGraphics().DrawLine(pen, mousePreviousControlPos.Value, mouse);

                    mousePreviousControlPos = mouse;
                }
            }
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
