
using System.Collections.Generic;
using System.Linq;

namespace CaptureImage.Common
{
    public class DrawingContextsKeeper
    {
        private ChangesHistory changesHistory;
        public List<DrawingContext> DrawingContexts { get; set; }

        public DrawingContextsKeeper()
        {
            changesHistory = new ChangesHistory();
            DrawingContexts = new List<DrawingContext>();
        }

        public void SaveContext()
        {
            changesHistory.SaveChange(DrawingContexts.ToArray());
        }

        public void RevertToPreviousContext()
        {
            DrawingContexts = changesHistory.GetPrevious().ToList();
        }

    }
}
