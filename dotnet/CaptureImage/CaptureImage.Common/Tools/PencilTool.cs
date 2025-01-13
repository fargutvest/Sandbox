using System.Drawing;

namespace CaptureImage.Common.Tools
{
    public class PencilTool : ITool
    {
        private DrawingState state;
        private Point mousePreviousPos;
        private Pen pen;
        private bool isActive;
        private DrawingContext[] drawingContexts;

        public PencilTool(DrawingContext[] drawingContexts)
        {
            this.drawingContexts = drawingContexts;
            this.state = DrawingState.None;

            mousePreviousPos = new Point(0, 0);
            pen = new Pen(Color.Yellow)
            {
                Width = 2,
            };
        }

        public void MouseMove(Point mouse)
        {
            if (isActive)
            {
                if (state == DrawingState.Drawing)
                {
                    for (int i = 0; i < drawingContexts.Length; i++)
                    {
                        DrawingContext dc = drawingContexts[i];
                        Graphics.FromImage(dc.CanvasImage).DrawLine(pen, mousePreviousPos, mouse);
                        dc.CanvasControl.CreateGraphics().DrawLine(pen, mousePreviousPos, mouse);
                    }

                    mousePreviousPos = mouse;
                }
            }
        }

        public void MouseDown(Point mousePosition)
        {
            if (isActive)
            {
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
