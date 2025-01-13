using CaptureImage.Common.Tools.Misc;
using System.Drawing;
using System.Linq;

namespace CaptureImage.Common.Tools
{
    public class ArrowTool : ITool
    {
        private DrawingState state;
        private Point mouseStartPos;
        private Point mousePreviousPos;
        private Pen pen;
        private Pen[] erasePens;
        private bool isActive;

        private DrawingContext[] drawingContexts;

        public ArrowTool(DrawingContext[] drawingContexts)
        {
            this.drawingContexts = drawingContexts;
            this.state = DrawingState.None;

            mouseStartPos = new Point(0, 0);
            pen = new Pen(Color.Violet) { Width = 2 };
        }

        public void MouseDown(Point mousePosition)
        {
            if (isActive)
            {
                erasePens = drawingContexts.Select(dc => new Pen(new TextureBrush(dc.CanvasImage)) { Width = 2 }).ToArray();
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
                    for (int i = 0; i < drawingContexts.Length; i++)
                    {
                        DrawingContext dc = drawingContexts[i];
                        Graphics.FromImage(dc.CanvasImage).DrawLine(erasePens[i], mouseStartPos, mousePreviousPos);
                        dc.CanvasControl.CreateGraphics().DrawLine(erasePens[i], mouseStartPos, mousePreviousPos);
                    }

                    for (int i = 0; i < drawingContexts.Length; i++)
                    {
                        DrawingContext dc = drawingContexts[i];
                        Graphics.FromImage(dc.CanvasImage).DrawLine(pen, mouseStartPos, mouse);
                        dc.CanvasControl.CreateGraphics().DrawLine(pen, mouseStartPos, mouse);
                    }

                    mousePreviousPos = mouse;
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
