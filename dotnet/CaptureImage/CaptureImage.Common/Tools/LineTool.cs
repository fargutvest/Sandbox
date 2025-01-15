using CaptureImage.Common.Tools.Misc;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CaptureImage.Common.Tools
{
    public class LineTool : ITool
    {
        protected DrawingState state;
        protected Point mouseStartPos;
        private Point mousePreviousPos;
        protected Pen pen;
        protected Pen[] erasePens;
        protected bool isActive;

        protected DrawingContextsKeeper drawingContextsKeeper;
        

        public LineTool(DrawingContextsKeeper drawingContextsKeeper)
        {
            this.drawingContextsKeeper = drawingContextsKeeper;
            this.state = DrawingState.None;

            mouseStartPos = new Point(0, 0);
            pen = new Pen(Color.Yellow) { Width = 2 };
        }

        public virtual void MouseDown(Point mousePosition)
        {
            if (isActive)
            {
                erasePens = drawingContextsKeeper.DrawingContext.CanvasImages.Select(im => new Pen(new TextureBrush(im)) { Width = 2 }).ToArray();
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
                    for (int i = 0; i < drawingContextsKeeper.DrawingContext.CanvasImages.Length; i++)
                    {
                        Image im = drawingContextsKeeper.DrawingContext.CanvasImages[i];
                        Control ct = drawingContextsKeeper.DrawingContext.CanvasControls[i];
                        Graphics.FromImage(im).DrawLine(erasePens[i], mouseStartPos, mousePreviousPos);
                        ct.CreateGraphics().DrawLine(erasePens[i], mouseStartPos, mousePreviousPos);
                    }

                    for (int i = 0; i < drawingContextsKeeper.DrawingContext.CanvasImages.Length; i++)
                    {
                        Image im = drawingContextsKeeper.DrawingContext.CanvasImages[i];
                        Control ct = drawingContextsKeeper.DrawingContext.CanvasControls[i];

                        Graphics.FromImage(im).DrawLine(pen, mouseStartPos, mouse);
                        ct.CreateGraphics().DrawLine(pen, mouseStartPos, mouse);
                        drawingContextsKeeper.DrawingContext.IsClean = false;
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
