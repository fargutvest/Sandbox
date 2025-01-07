using System.Drawing;
using System.Windows.Forms;

namespace CaptureImage.WinForms
{
    public partial class ScreenBase : Form
    {
        public ScreenBase()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.DoubleBuffered = true;
        }
    }
}
