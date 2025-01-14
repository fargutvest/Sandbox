using CaptureImage.Common.Tools.Misc;
using System.Drawing;


namespace CaptureImage.Common.Tools
{
    public class PencilTool : ITool
    {
        private DrawingState state;
        private Point mousePreviousPos;
        private Pen pen;
        private bool isActive;
        private DrawingContextsKeeper drawingContextsKeeper;

        public PencilTool(DrawingContextsKeeper drawingContextsKeeper)
        {
            this.drawingContextsKeeper = drawingContextsKeeper;
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
                    for (int i = 0; i < drawingContextsKeeper.DrawingContexts.Count; i++)
                    {
                        DrawingContext dc = drawingContextsKeeper.DrawingContexts[i];
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
                drawingContextsKeeper.SaveContext();
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
