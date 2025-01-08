using CaptureImage.Common.Helpers;
using System.Drawing;
using System.Windows.Forms;
using CaptureImage.Common;

namespace CaptureImage.WinForms
{
    public partial class Thumb : UserControl, IThumb
    {
        public Rectangle[] HandleRectangles { get; private set; }

        public Thumb()
        {
            InitializeComponent();

            HandleRectangles = new Rectangle[0];
        }

        private void Thumb_Paint(object sender, PaintEventArgs e)
        {
            int handleSize = 5;
            int padding = 2;
            Rectangle rect = new Rectangle(handleSize / 2 + padding, handleSize / 2 + padding, this.Width - handleSize - padding * 2, this.Height - handleSize - padding * 2);

            HandleRectangles = GraphicsHelper.DrawSelectionBorder(e.Graphics, rect, handleSize);

            for (int i = 0; i< HandleRectangles.Length; i++)
            {
                HandleRectangles[i].Offset(this.Location);
            }
        }
    }
}
