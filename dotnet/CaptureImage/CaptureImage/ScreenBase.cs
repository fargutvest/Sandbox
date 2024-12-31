using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage
{
    public partial class ScreenBase : Form
    {
        public ScreenBase(Size clientSize, Point location)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.DoubleBuffered = true;
            this.ClientSize = clientSize;
            this.Location = location;
        }
    }
}
