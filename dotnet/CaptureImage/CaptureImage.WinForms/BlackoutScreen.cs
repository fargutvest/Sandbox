using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using CaptureImage.Common.Tools;
using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.WinForms
{
    public partial class BlackoutScreen : ScreenBase
    {
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
            //TopMost = true 

            selectingTool = new SelectingTool();

            this.thumb = new Thumb();
            this.thumb.Size = new Size(0,0);
            this.Controls.Add(thumb);
        }

        private void BlackoutScreen_MouseMove(object sender, MouseEventArgs e)
        {
            selectingTool.ChangeSelecting(e.Location);
            selectingTool.Pulse(thumb);
        }

        private void BlackoutScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectingTool.StopSelecting(e.Location);
            }
        }

        private void BlackoutScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectingTool.StartSelecting(e.Location);
            }
        }

    }
}
