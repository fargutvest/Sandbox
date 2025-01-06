using CaptureImage.Common.Extensions;
using CaptureImage.Common.Helpers;
using System.Drawing;

namespace CaptureImage.Common.Tools
{
    public class SelectingTool
    {
        private bool isSelecting;
        private Rectangle selectingRect;
        private Pen selectingPen;
        private Pen selectingPen2;
        private Point selectingMousePos;
        private Point selectingMouseStartPos;

        public SelectingTool()
        {
            selectingPen = new Pen(Color.Yellow)
            {
                Width = 2,
                DashPattern = new float[] { 5, 5 }
            };
            selectingPen2 = new Pen(Color.Violet)
            {
                Width = 2
            };
        }

        public void Paint(Graphics gr, Bitmap background)
        {
            gr.DrawImage(background, selectingRect, selectingRect, GraphicsUnit.Pixel);

            GraphicsHelper.DrawBorder(gr, selectingRect, selectingPen, selectingPen2);
        }

        public void StartSelecting(Point startSelectingPoint)
        {
            if (selectingRect.Contains(startSelectingPoint) == false)
            {
                isSelecting = true;
                selectingMouseStartPos = startSelectingPoint;
            }
        }

        public void ChangeSelecting(Point mousePosition)
        {
            if (isSelecting)
            {
                UpdateSelectingRect(mousePosition);
            }
        }

        public void StopSelecting(Point mousePosition)
        {
            if (isSelecting)
            {
                isSelecting = false;
                UpdateSelectingRect(mousePosition);
            }
        }

        public void HideSelecting()
        {
            selectingRect = Rectangle.Empty;
        }

        public void Select(Rectangle selectingRect)
        {
            this.selectingRect = selectingRect;
        }

        public void UpdateSelectingRect(Point mousePosition)
        {
            selectingMousePos = mousePosition;

            selectingRect = new Rectangle(
                selectingMouseStartPos.X, selectingMouseStartPos.Y,
                selectingMousePos.X - selectingMouseStartPos.X, selectingMousePos.Y - selectingMouseStartPos.Y
            );

            if (selectingRect.Width < 0)
            {
                selectingRect.X = selectingRect.X + selectingRect.Width;
                selectingRect.Width = -selectingRect.Width;
            }


            if (selectingRect.Height < 0)
            {
                selectingRect.Y = selectingRect.Y + selectingRect.Height;
                selectingRect.Height = -selectingRect.Height;
            }
        }
    }
}
