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
        private Point mousePos;
        private Point mouseStartPos;

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

            GraphicsHelper.DrawSelectionBorder(gr, selectingRect);
        }

        public void Pulse(Control control)
        {
            if (isSelecting)
            {
                control.Visible = false;

                int thumbWidth = Math.Abs(mousePos.X - mouseStartPos.X);
                int thumbHeight = Math.Abs(mousePos.Y - mouseStartPos.Y);
                control.Size = new Size(thumbWidth, thumbHeight);

                if (mouseStartPos.X < mousePos.X && mouseStartPos.Y < mousePos.Y)
                    control.Location = new Point(mouseStartPos.X, mouseStartPos.Y);

                if (mouseStartPos.X > mousePos.X && mouseStartPos.Y < mousePos.Y)
                    control.Location = new Point(mousePos.X, mouseStartPos.Y);

                if (mouseStartPos.X < mousePos.X && mouseStartPos.Y > mousePos.Y)
                    control.Location = new Point(mouseStartPos.X, mousePos.Y);

                if (mouseStartPos.X > mousePos.X && mouseStartPos.Y > mousePos.Y)
                    control.Location = new Point(mousePos.X, mousePos.Y);


                control.Visible = true;
            }
        }

        public void MouseDown(Point startSelectingPoint)
        {
            if (selectingRect.Contains(startSelectingPoint) == false)
            {
                isSelecting = true;
                mouseStartPos = startSelectingPoint;
            }
        }

        public void MouseMove(Point mousePosition)
        {
            if (isSelecting)
            {
                UpdateSelectingRect(mousePosition);
            }
        }

        public void MouseUp(Point mousePosition)
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
            mousePos = mousePosition;

            selectingRect = new Rectangle(
                mouseStartPos.X, mouseStartPos.Y,
                mousePos.X - mouseStartPos.X, mousePos.Y - mouseStartPos.Y
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
