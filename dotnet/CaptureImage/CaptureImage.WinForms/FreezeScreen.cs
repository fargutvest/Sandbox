using System.Drawing;

namespace CaptureImage.WinForms
{
    public partial class FreezeScreen : ScreenBase
    {
        public FreezeScreen(Size clientSize, Point location) : base(clientSize, location)
        {
            InitializeComponent();
        }
    }
}
