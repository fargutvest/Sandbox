using System.Drawing.Drawing2D;


namespace CaptureImage.Common.Tools
{
    public class ArrowTool : LineTool
    {
        private DrawingContextsKeeper drawingContextsKeeper;

        public ArrowTool(DrawingContextsKeeper drawingContextsKeeper) : base (drawingContextsKeeper)
        {
            CustomLineCap cap = new AdjustableArrowCap(4, 7);
            pen.CustomEndCap = cap;
        }
    }
}
