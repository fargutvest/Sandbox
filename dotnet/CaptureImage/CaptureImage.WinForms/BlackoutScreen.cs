using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using CaptureImage.Common.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;
using CaptureImage.Common.Extensions;

namespace CaptureImage.WinForms
{
    public partial class BlackoutScreen : ScreenBase
    {
        private bool isInit = true;

        private Thumb thumb;

        private DescktopInfo desktopInfo;

        private SelectingTool selectingTool;

        public BlackoutScreen()
        {
            InitializeComponent();

            desktopInfo = ScreensHelper.GetDesktopInfo();
            ClientSize = desktopInfo.ClientSize;
            Location = desktopInfo.Location;
            BackColor = Color.Black;
            BackgroundImage = BitmapHelper.ChangeOpacity(desktopInfo.Background, 0.5f);
            TransparencyKey = Color.Red;
            Region = new Region(desktopInfo.Path);
            //TopMost = true;

            selectingTool = new SelectingTool();

            this.thumb = new Thumb();
            this.thumb.Size = new Size(0,0);
            this.Controls.Add(thumb);
        }
        private void BlackoutScreen_Load(object sender, System.EventArgs e)
        {
            Timer timer = new Timer
            {
                Interval = 10
            };
            timer.Tick += new EventHandler(MouseMoveEvent);
           // TODO: Что работает быстрее ? Обновление позиции мыши и перерисовка по таймеру, или по событию MouseMove ?
           // timer.Start();
        }

        private void MouseMoveEvent(object sender, EventArgs e)
        {
            selectingTool.MouseMove(this.GetMousePosition(), this);
        }

        private void BlackoutScreen_MouseMove(object sender, MouseEventArgs e)
        {
            // TODO: Что работает быстрее ? Обновление позиции мыши и перерисовка по таймеру, или по событию MouseMove ?
            selectingTool.MouseMove(e.Location, this);
            selectingTool.Pulse(thumb);
        }

        private void BlackoutScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectingTool.MouseUp(e.Location);
            }
        }

        private void BlackoutScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectingTool.MouseDown(e.Location);
            }
        }

        private void BlackoutScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                selectingTool.Select(desktopInfo.BackgroundRect);
            }
        }


        private void BlackoutScreen_Paint(object sender, PaintEventArgs e)
        {
            if (isInit)
            {
                isInit = false;
            }

            // TODO: Что работает быстрее ? Обновление позиции мыши и перерисовка по таймеру, или по событию MouseMove ?
            // selectingTool.Pulse(thumb);
        }
    }
}
