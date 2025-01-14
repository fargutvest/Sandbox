

namespace CaptureImage.Common
{
    public class DrawingContextsKeeper
    {
        private ChangesHistory changesHistory;
        public DrawingContext DrawingContext { get; set; }

        public DrawingContextsKeeper()
        {
            changesHistory = new ChangesHistory();
        }

        public void SaveContext()
        {
            changesHistory.SaveChange(DrawingContext);
        }

        public void RevertToPreviousContext()
        {
            DrawingContext = changesHistory.GetPrevious();

        }

    }
}
