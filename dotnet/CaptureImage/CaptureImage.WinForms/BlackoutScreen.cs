using System;
using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.WinForms
{
    public partial class BlackoutScreen : ScreenBase
    {
        private int mouse_x;
        private int mouse_y;
        private int start_mouse_x;
        private int start_mouse_y;

        private bool lmb_down;
        private Thumb thumb;

        public BlackoutScreen(Size clientSize, Point location) : base(clientSize, location)
        {
            InitializeComponent();
            this.thumb = new Thumb();
            this.thumb.Size = new Size(0,0);
            this.Controls.Add(thumb);
        }

        private void SplashScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (lmb_down)
            {
                thumb.Visible = false;

                int thumbWidth = Math.Abs(e.X - start_mouse_x);
                int thumbHeight = Math.Abs(e.Y - start_mouse_y);
                thumb.Size = new Size(thumbWidth, thumbHeight);

                if (start_mouse_x < e.X && start_mouse_y < e.Y)
                    thumb.Location = new Point(start_mouse_x, start_mouse_y);

                if (start_mouse_x > e.X && start_mouse_y < e.Y)
                    thumb.Location = new Point(e.X, start_mouse_y);

                if (start_mouse_x < e.X && start_mouse_y > e.Y)
                    thumb.Location = new Point(start_mouse_x, e.Y);

                if (start_mouse_x > e.X && start_mouse_y > e.Y)
                    thumb.Location = new Point(e.X, e.Y);


                thumb.Visible = true;
            }
        }

        private void SplashScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lmb_down = false;
            }
        }

        private void SplashScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.thumb.Size = new Size(0,0);

                lmb_down = true;
                start_mouse_x = e.X;
                start_mouse_y = e.Y;
            }
        }

    }
}
