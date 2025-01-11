using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.Common.Tools
{
    public class DrawingTool
    {
        private Point mousePreviousPos;

        private Pen pen;

        private bool isActive;

        public DrawingTool()
        {
            mousePreviousPos = new Point(0, 0);
            pen = new Pen(Color.Yellow)
            {
                Width = 1,
            };
        }

        public void Pulse(Graphics gr, Point mouse)
        {
            if (isActive)
            {
                gr.DrawLine(pen, mousePreviousPos, mouse);
                mousePreviousPos = mouse;
            }
        }

        public void MouseDown(Point mousePosition)
        {
            mousePreviousPos = mousePosition;
            isActive = true;
        }

        public void MouseUp(Point mousePosition)
        {
            isActive = false;
        }

        public void MouseMove(Graphics gr, Point mousePosition, Control canvas)
        {

        }

    }
}
