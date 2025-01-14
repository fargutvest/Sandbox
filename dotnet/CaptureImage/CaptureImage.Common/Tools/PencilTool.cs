using CaptureImage.Common.Tools.Misc;
using System.Drawing;
using System.Windows.Forms;


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
                    for (int i = 0; i < drawingContextsKeeper.DrawingContext.CanvasImages.Length; i++)
                    {
                        Image im = drawingContextsKeeper.DrawingContext.CanvasImages[i];
                        Control ct = drawingContextsKeeper.DrawingContext.CanvasControls[i];

                        Graphics.FromImage(im).DrawLine(pen, mousePreviousPos, mouse);
                        ct.CreateGraphics().DrawLine(pen, mousePreviousPos, mouse);
                        drawingContextsKeeper.DrawingContext.IsClean = false;
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
