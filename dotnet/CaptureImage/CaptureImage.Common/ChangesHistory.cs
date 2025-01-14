using System.Collections.Generic;

namespace CaptureImage.Common
{
    public class ChangesHistory
    {
        private Stack<DrawingContext> changes;

        public ChangesHistory()
        {
            changes = new Stack<DrawingContext>();
        }

        public DrawingContext GetPrevious()
        {
            if (changes.Count > 1)
                changes.Pop();

            if (changes.Count == 0)
                return new DrawingContext();

            DrawingContext drawingContexts = changes.Peek();

            return drawingContexts.Clone() as DrawingContext;
        }

        public DrawingContext GetCurrent()
        {
            return changes.Peek();
        }

        public void SaveChange(DrawingContext drawingContext)
        {
            DrawingContext clone = drawingContext.Clone() as DrawingContext;
            changes.Push(clone);
        }
    }
}
