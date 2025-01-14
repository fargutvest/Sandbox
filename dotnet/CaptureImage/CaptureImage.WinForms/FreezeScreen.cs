using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using System.Drawing;

namespace CaptureImage.WinForms
{
    public partial class FreezeScreen : ScreenBase
    {
        private DescktopInfo desktopInfo;

        public FreezeScreen(AppContext appContext)
        {
            InitializeComponent();
            appContext.AddControl(this, isCanvas: true);
            desktopInfo = ScreensHelper.GetDesktopInfo();
            ClientSize = desktopInfo.ClientSize;
            Location = desktopInfo.Location;
            BackgroundImage = desktopInfo.Background;
            Region = new Region(desktopInfo.Path);
        }
    }
}
