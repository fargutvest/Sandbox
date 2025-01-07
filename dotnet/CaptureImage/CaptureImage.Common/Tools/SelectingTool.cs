using CaptureImage.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CaptureImage.Common.Tools
{
    public class SelectingTool
    {
        private bool isSelecting;

        private Rectangle selectingRect;
        private Point mousePos;
        private Point mouseStartPos;

        private Rectangle[] handleRectangles;

        private Dictionary<int, Cursor> handleCursors = new Dictionary<int, Cursor>();

        private bool IsHandleHovered => handleRectangles.Any(rect => rect.Contains(mousePos));

        private bool IsSelectingRectangleHovered => selectingRect.Contains(mousePos);

        public SelectingTool()
        {
            handleCursors.Add(0, Cursors.SizeNWSE); /// угол
            handleCursors.Add(1, Cursors.SizeNS);
            handleCursors.Add(2, Cursors.SizeNESW); // угол
            handleCursors.Add(3, Cursors.SizeWE);
            handleCursors.Add(4, Cursors.SizeNWSE); // угол
            handleCursors.Add(5, Cursors.SizeNS);
            handleCursors.Add(6, Cursors.SizeNESW); // угол
            handleCursors.Add(7, Cursors.SizeWE);
        }

        public void Pulse(Graphics gr, Bitmap background)
        {
            gr.DrawImage(background, selectingRect, selectingRect, GraphicsUnit.Pixel);

            handleRectangles = GraphicsHelper.DrawSelectionBorder(gr, selectingRect);
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

        public void MouseDown(Point mousePosition)
        {
            if (selectingRect.IsEmpty || IsSelectingRectangleHovered == false)
            {
                isSelecting = true;
            }
            mouseStartPos = mousePosition;
        }

        public void MouseUp(Point mousePosition)
        {
            isSelecting = false;
            mousePos = mousePosition;
            UpdateSelectingRect();
        }


        public void MouseMove(Point mousePosition, Control control)
        {
            mousePos = mousePosition;
            if (isSelecting)
            {
                UpdateSelectingRect();
            }
            else if (IsHandleHovered)
            {
                Rectangle hoveredHandleRect = handleRectangles.First(rect => rect.Contains(mousePos));
                int rectangleIndex = handleRectangles.ToList().IndexOf(hoveredHandleRect);
                control.Cursor = handleCursors[rectangleIndex];
            }
            else if (IsSelectingRectangleHovered)
            {
                control.Cursor = Cursors.SizeAll;
            }
            else
            {
                control.Cursor = Cursors.Default;
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

        private void UpdateSelectingRect()
        {
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
