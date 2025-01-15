using CaptureImage.Common.Tools.Misc;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Drawing;

namespace CaptureImage.Common.Tools
{
    public class ArrowTool : LineTool
    {
        private CustomLineCap endCap;

        public ArrowTool(DrawingContextsKeeper drawingContextsKeeper) : base (drawingContextsKeeper)
        {
            endCap = new AdjustableArrowCap(4, 7);
            pen.CustomEndCap = endCap;
        }

        public override void MouseDown(Point mousePosition)
        {
            if (isActive)
            {
                erasePens = drawingContextsKeeper.DrawingContext.CanvasImages.Select(im => new Pen(new TextureBrush(im))
                {
                    Width = 2,
                    CustomEndCap = endCap
                }).ToArray();

                mouseStartPos = mousePosition;
                state = DrawingState.Drawing;
            }
        }
    }
}
