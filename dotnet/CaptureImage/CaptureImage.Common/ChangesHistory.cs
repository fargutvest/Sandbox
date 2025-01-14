using System.Collections.Generic;
using System.Linq;

namespace CaptureImage.Common
{
    public class ChangesHistory
    {
        private Stack<DrawingContext[]> changes;

        public ChangesHistory()
        {
            changes = new Stack<DrawingContext[]>();
        }

        public DrawingContext[] GetPrevious()
        {
            if (changes.Count > 1)
                changes.Pop();

            if (changes.Count == 0)
                return new DrawingContext[0];

            DrawingContext[] drawingContexts = changes.Peek();

            return drawingContexts.Select(dc => dc.Clone() as DrawingContext).ToArray();
        }

        public DrawingContext[] GetCurrent()
        {
            return changes.Peek();
        }

        public void SaveChange(DrawingContext[] drawingContexts)
        {
            DrawingContext[] clones = drawingContexts.Select(dc => dc.Clone() as DrawingContext).ToArray();
            changes.Push(clones);
        }
    }
}
