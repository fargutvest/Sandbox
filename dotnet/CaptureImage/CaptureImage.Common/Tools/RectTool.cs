using CaptureImage.Common.Tools.Misc;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
                        Rectangle rect = new Rectangle(mouseStartPos,  new Size(mousePreviousPos) - new Size(mouseStartPos));

                        Image im = drawingContextsKeeper.DrawingContext.CanvasImages[i];
                        Control ct = drawingContextsKeeper.DrawingContext.CanvasControls[i];
                        Graphics.FromImage(im).DrawRectangle(erasePens[i], rect);
                        ct.CreateGraphics().DrawRectangle(erasePens[i], rect);
                    }

                    for (int i = 0; i < drawingContextsKeeper.DrawingContext.CanvasImages.Length; i++)
                    {
                        Rectangle rect = new Rectangle(mouseStartPos,  new Size(mouse) - new Size(mouseStartPos));

                        Image im = drawingContextsKeeper.DrawingContext.CanvasImages[i];
                        Control ct = drawingContextsKeeper.DrawingContext.CanvasControls[i];
                        Graphics.FromImage(im).DrawRectangle(pen, rect);
                        ct.CreateGraphics().DrawRectangle(pen, rect);
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
