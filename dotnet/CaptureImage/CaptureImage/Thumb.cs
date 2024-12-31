using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage
{
    public partial class Thumb : UserControl
    {
        private bool lmb_down;
        private int start_mouse_x;
        private int start_mouse_y;

        private bool mouseIsOver;

        public Thumb()
        {
            InitializeComponent();
        }

        private bool IsVerticalBorderHovered(MouseEventArgs e, int borderThickness)
        {
            // курсор мыши находится над левой границей
            if (e.X >= 0 && e.X <= borderThickness)
                return true;

            // курсор мыши находится над правой границей
            if (e.X >= (this.Width - borderThickness) && e.X <= this.Width)
                return true;


            return false;
        }

        private bool IsHorizontalBorderHovered(MouseEventArgs e, int borderThickness)
        {
            // курсор мыши находится над верхней границей
            if (e.Y >= 0 && e.Y <= borderThickness)
                return true;

            // курсор мыши находится над нижней границей
            if (e.Y >= (this.Height - borderThickness) && e.Y <= this.Height)
                return true;


            return false;
        }

        private void Thumb_MouseMove(object sender, MouseEventArgs e)
        {
            int borderThickness = 3;
            this.Cursor = Cursors.SizeAll;

            if (lmb_down)
            {
                this.Location = new Point(this.Location.X + e.X - start_mouse_x, this.Location.Y + e.Y - start_mouse_y);
            }
            else
            {
                if (IsVerticalBorderHovered(e, borderThickness))
                    this.Cursor = Cursors.SizeWE;

                if (IsHorizontalBorderHovered(e, borderThickness))
                    this.Cursor = Cursors.SizeNS;


                else if (e.X == 0 && e.Y == 0)
                {
                    this.Cursor = Cursors.SizeNESW;
                }
            }
        }

        private void Thumb_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lmb_down = false;
            }
        }

        private void Thumb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lmb_down = true;
                start_mouse_x = e.X;
                start_mouse_y = e.Y;
            }
        }


        private void Thumb_MouseLeave(object sender, System.EventArgs e)
        {
            mouseIsOver = false;
        }

        private void Thumb_MouseEnter(object sender, System.EventArgs e)
        {
            mouseIsOver = true;
        }

        private void MouseNearBorder()
        {

        }



    }
}
