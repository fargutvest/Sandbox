using System.Collections.Generic;
using System.Drawing;

namespace CaptureImage.Common.Helpers
{
    public static class GraphicsHelper
    {
        public static Rectangle[] DrawSelectionBorder(Graphics gr, Rectangle rect, int handleSize = 5)
        {
            Pen pen = new Pen(Color.White)
            {
                Width = 1,
                DashPattern = new float[] { 3, 3 }
            };

            gr.DrawRectangle(pen, rect);

            int halfHandleSizeMin = handleSize / 2;
            int halfHandleSizeMax = halfHandleSizeMin + handleSize % 2;
            int handleSizeDiff = halfHandleSizeMax - halfHandleSizeMin;

            int halfRectWidthMin = rect.Width / 2;
            int halfRectWidthMax = halfRectWidthMin + rect.Width % 2;
            int halfRectWidthDiff = halfRectWidthMax - halfRectWidthMin;

            int halfRectHeightMin = rect.Height / 2;
            int halfRectHeightMax = halfRectHeightMin + rect.Height % 2;
            int halfRectHeightDiff = halfRectHeightMax - halfRectHeightMin;

            int antiShakeOffsetX = halfRectWidthDiff == 0 ? handleSizeDiff : handleSizeDiff * 2;
            int antiShakeOffsetY = halfRectHeightDiff == 0 ? handleSizeDiff : handleSizeDiff * 2;

            var rectangles = new List<Rectangle>();
            var r1 = new Rectangle(rect.Left - halfHandleSizeMax, rect.Top - halfHandleSizeMax, handleSize, handleSize); rectangles.Add(r1); // угол
            r1.Offset(rect.Width / 2, 0); rectangles.Add(r1);
            r1.Offset(rect.Width / 2 + antiShakeOffsetX, 0); rectangles.Add(r1); // угол
            r1.Offset(0, rect.Height / 2 + antiShakeOffsetY); rectangles.Add(r1);
            r1.Offset(0, rect.Height / 2); rectangles.Add(r1); // угол
            r1.Offset(-rect.Width / 2 - antiShakeOffsetX, 0); rectangles.Add(r1);
            r1.Offset(-rect.Width / 2, 0); rectangles.Add(r1); // угол
            r1.Offset(0, -rect.Height / 2); rectangles.Add(r1);
            gr.FillRectangles(Brushes.Black, rectangles.ToArray());
            gr.DrawRectangles(Pens.White, rectangles.ToArray());

            return rectangles.ToArray();
        }
    }
}
