using CaptureImage.Common.Tools.Misc;
using System.Drawing;
using System.Linq;

namespace CaptureImage.Common.Tools
{
    public class RectTool : ITool
    {
        private DrawingState state;
        private Point mouseStartPos;
        private Point mousePreviousPos;
        private Pen pen;
        private Pen[] erasePens;
        private bool isActive;

        private DrawingContextsKeeper drawingContextsKeeper;

        public RectTool(DrawingContextsKeeper drawingContextsKeeper)
        {
            this.drawingContextsKeeper = drawingContextsKeeper;
            this.state = DrawingState.None;

            mouseStartPos = new Point(0, 0);
            pen = new Pen(Color.Yellow) { Width = 2 };
        }

        public void MouseDown(Point mousePosition)
        {
            if (isActive)
            {
                erasePens = drawingContextsKeeper.DrawingContexts.Select(dc => new Pen(new TextureBrush(dc.CanvasImage)) { Width = 2 }).ToArray();
                mouseStartPos = mousePosition;
                state = DrawingState.Drawing;
            }
        }

        public void MouseMove(Point mouse)
        {
            if (isActive)
            {
                if (state == DrawingState.Drawing)
                {
                    for (int i = 0; i < drawingContextsKeeper.DrawingContexts.Count; i++)
                    {
                        Rectangle rect = new Rectangle(mouseStartPos,  new Size(mousePreviousPos) - new Size(mouseStartPos));

                        DrawingContext dc = drawingContextsKeeper.DrawingContexts[i];
                        Graphics.FromImage(dc.CanvasImage).DrawRectangle(erasePens[i], rect);
                        dc.CanvasControl.CreateGraphics().DrawRectangle(erasePens[i], rect);
                    }

                    for (int i = 0; i < drawingContextsKeeper.DrawingContexts.Count; i++)
                    {
                        Rectangle rect = new Rectangle(mouseStartPos,  new Size(mouse) - new Size(mouseStartPos));

                        DrawingContext dc = drawingContextsKeeper.DrawingContexts[i];
                        Graphics.FromImage(dc.CanvasImage).DrawRectangle(pen, rect);
                        dc.CanvasControl.CreateGraphics().DrawRectangle(pen, rect);
                    }

                    mousePreviousPos = mouse;
                }
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
