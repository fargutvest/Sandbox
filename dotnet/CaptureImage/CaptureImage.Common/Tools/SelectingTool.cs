using CaptureImage.Common.Extensions;
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
        private SelectingState selectingState;

        private Rectangle selectingRect;
        private Point mousePos;
        private Point mouseStartPos;
        private Point relativeMouseStartPos;
        private Rectangle[] handleRectangles;
        private int hoveredHandleIndex;
        private Rectangle selectingRectResizeStart;
        private Dictionary<int, Cursor> handleCursors;

        private bool IsHandleHovered => handleRectangles.Any(rect => rect.Contains(mousePos));

        private bool IsSelectingRectangleHovered => selectingRect.Contains(mousePos);

        public SelectingTool()
        {
            handleCursors = new Dictionary<int, Cursor>
            {
                { 0, Cursors.SizeNWSE },/// угол
                { 1, Cursors.SizeNS },
                { 2, Cursors.SizeNESW }, // угол
                { 3, Cursors.SizeWE },
                { 4, Cursors.SizeNWSE }, // угол
                { 5, Cursors.SizeNS },
                { 6, Cursors.SizeNESW }, // угол
                { 7, Cursors.SizeWE }
            };
        }

        public void Pulse(Graphics gr, Bitmap background)
        {
            gr.DrawImage(background, selectingRect, selectingRect, GraphicsUnit.Pixel);

            handleRectangles = GraphicsHelper.DrawSelectionBorder(gr, selectingRect);
        }

        public void Pulse(Control selector)
        {
            selector.Visible = false;

            selector.Size = new Size(Math.Abs(mousePos.X - mouseStartPos.X), Math.Abs(mousePos.Y - mouseStartPos.Y));

            if (mouseStartPos.X < mousePos.X && mouseStartPos.Y < mousePos.Y)
                selector.Location = new Point(mouseStartPos.X, mouseStartPos.Y);

            if (mouseStartPos.X > mousePos.X && mouseStartPos.Y < mousePos.Y)
                selector.Location = new Point(mousePos.X, mouseStartPos.Y);

            if (mouseStartPos.X < mousePos.X && mouseStartPos.Y > mousePos.Y)
                selector.Location = new Point(mouseStartPos.X, mousePos.Y);

            if (mouseStartPos.X > mousePos.X && mouseStartPos.Y > mousePos.Y)
                selector.Location = new Point(mousePos.X, mousePos.Y);


            selector.Visible = true;

        }

        public void MouseDown(Point mousePosition)
        {
            mouseStartPos = mousePosition;

            if ((selectingRect.IsEmpty || IsSelectingRectangleHovered == false) && IsHandleHovered == false)
            {
                selectingState = SelectingState.Selecting;
            }
            else if (selectingRect.IsEmpty == false && IsSelectingRectangleHovered && IsHandleHovered == false)
            {
                selectingState = SelectingState.Moving;

                relativeMouseStartPos = new Point( mousePosition.X - selectingRect.X, mousePosition.Y - selectingRect.Y);
            }
            else if (selectingRect.IsEmpty == false && IsHandleHovered)
            {
                selectingState = SelectingState.Resizing;

                Rectangle hoveredHandleRect = handleRectangles.First(rect => rect.Contains(mousePos));
                hoveredHandleIndex = handleRectangles.ToList().IndexOf(hoveredHandleRect);
                selectingRectResizeStart = selectingRect.Clone();
            }
        }

        public void MouseUp(Point mousePosition)
        {
            if (selectingState == SelectingState.Selecting)
            {
                mousePos = mousePosition;
                UpdateSelectingRect();
            }

            selectingState = SelectingState.None;
        }


        public void MouseMove(Point mousePosition, Control canvas)
        {
            mousePos = mousePosition;
    
            if (IsHandleHovered)
            {
                Rectangle hoveredHandleRect = handleRectangles.First(rect => rect.Contains(mousePos));
                int rectangleIndex = handleRectangles.ToList().IndexOf(hoveredHandleRect);
                canvas.Cursor = handleCursors[rectangleIndex];
            }
            else if (IsSelectingRectangleHovered)
            {
                canvas.Cursor = Cursors.SizeAll;
            }
            else
            {
                canvas.Cursor = Cursors.Default;
            }

            switch (selectingState)
            {
                case SelectingState.Selecting:
                    UpdateSelectingRect();
                    break;
                case SelectingState.Moving:
                    MoveSelectingRect();
                    break;
                case SelectingState.Resizing:
                    ResizeSelectingRect();
                    break;
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

        private void MoveSelectingRect()
        {
            selectingRect = new Rectangle(mousePos.X - relativeMouseStartPos.X, mousePos.Y - relativeMouseStartPos.Y, selectingRect.Width, selectingRect.Height);
        }

        private void ResizeSelectingRect()
        {

            int deltaX = mousePos.X - mouseStartPos.X;
            int deltaY = mousePos.Y - mouseStartPos.Y;

            switch (hoveredHandleIndex)
            {
                case 0: // угол
                    selectingRect = new Rectangle(selectingRectResizeStart.X + deltaX, selectingRectResizeStart.Y + deltaY,
                        selectingRectResizeStart.Width - deltaX, selectingRectResizeStart.Height - deltaY);
                    break;

                case 1:
                    selectingRect = new Rectangle(selectingRect.X, selectingRectResizeStart.Y + deltaY,
                      selectingRect.Width, selectingRectResizeStart.Height - deltaY);
                    break;

                case 2: // угол
                    selectingRect = new Rectangle(selectingRectResizeStart.X, selectingRectResizeStart.Y + deltaY,
                      selectingRectResizeStart.Width + deltaX, selectingRectResizeStart.Height - deltaY);
                    break;

                case 3:
                    selectingRect = new Rectangle(selectingRectResizeStart.X, selectingRect.Y,
                    selectingRectResizeStart.Width + deltaX, selectingRect.Height);
                    break;

                case 4: // угол
                    selectingRect = new Rectangle(selectingRectResizeStart.X, selectingRectResizeStart.Y,
                     selectingRectResizeStart.Width + deltaX, selectingRectResizeStart.Height + deltaY);
                    break;

                case 5:
                    selectingRect = new Rectangle(selectingRect.X, selectingRect.Y,
                        selectingRect.Width, selectingRectResizeStart.Height + deltaY);
                    break;

                case 6: // угол
                    selectingRect = new Rectangle(selectingRectResizeStart.X + deltaX, selectingRectResizeStart.Y,
                    selectingRectResizeStart.Width - deltaX, selectingRectResizeStart.Height + deltaY);
                    break;

                case 7:
                    selectingRect = new Rectangle(selectingRectResizeStart.X + deltaX, selectingRect.Y,
                    selectingRectResizeStart.Width - deltaX, selectingRect.Height);
                    break;

            }
        }
    }
}
