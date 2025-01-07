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
            Rectangle rect = new Rectangle(handleSize/2, handleSize/2, this.Width, this.Height);
            rect.Width = rect.Width - handleSize - 1;
            rect.Height = rect.Height - handleSize - 1;
            HandleRectangles = GraphicsHelper.DrawSelectionBorder(e.Graphics, rect, handleSize);

            for (int i = 0; i< HandleRectangles.Length; i++)
            {
                HandleRectangles[i].Offset(this.Location);
            }
        }
    }
}
