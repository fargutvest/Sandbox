using CaptureImage.Common.Extensions;
using CaptureImage.Common.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

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

        public void Pulse(Graphics gr, Bitmap background)
        {
            gr.DrawImage(background, selectingRect, selectingRect, GraphicsUnit.Pixel);

            GraphicsHelper.DrawBorder(gr, selectingRect, selectingPen, selectingPen2);
        }

        public void Pulse(Control control)
        {
            if (isSelecting)
            {
                control.Visible = false;

                int thumbWidth = Math.Abs(selectingMousePos.X - selectingMouseStartPos.X);
                int thumbHeight = Math.Abs(selectingMousePos.Y - selectingMouseStartPos.Y);
                control.Size = new Size(thumbWidth, thumbHeight);

                if (selectingMouseStartPos.X < selectingMousePos.X && selectingMouseStartPos.Y < selectingMousePos.Y)
                    control.Location = new Point(selectingMouseStartPos.X, selectingMouseStartPos.Y);

                if (selectingMouseStartPos.X > selectingMousePos.X && selectingMouseStartPos.Y < selectingMousePos.Y)
                    control.Location = new Point(selectingMousePos.X, selectingMouseStartPos.Y);

                if (selectingMouseStartPos.X < selectingMousePos.X && selectingMouseStartPos.Y > selectingMousePos.Y)
                    control.Location = new Point(selectingMouseStartPos.X, selectingMousePos.Y);

                if (selectingMouseStartPos.X > selectingMousePos.X && selectingMouseStartPos.Y > selectingMousePos.Y)
                    control.Location = new Point(selectingMousePos.X, selectingMousePos.Y);


                control.Visible = true;
            }
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

        private void UpdateSelectingRect(Point mousePosition)
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
