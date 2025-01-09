using CaptureImage.Common.Helpers;
using System.Drawing;
using System.Windows.Forms;
using CaptureImage.Common;
using CaptureImage.Common.Extensions;

namespace CaptureImage.WinForms
{
    public partial class Thumb : UserControl, IThumb
    {
        public Rectangle[] HandleRectangles { get; private set; }

        private Label displaySizeLabel;
        private Panel panelY;
        private Panel panelX;

        public Control[] Components { get; }

        public Thumb()
        {
            InitializeComponent();

            // displaySizeLabel
            this.displaySizeLabel = new Label();
            this.displaySizeLabel.AutoSize = true;
            this.displaySizeLabel.Font = new Font(displaySizeLabel.Font.FontFamily, 10);
            this.displaySizeLabel.ForeColor = Color.White;

            // panelX
            this.panelX = new Panel();
            this.panelX.BackColor = System.Drawing.SystemColors.Control;
            this.panelX.Size = new Size(200, 30);
            this.panelX.SetRoundedShape(10);

            // panelY
            this.panelY = new Panel();
            this.panelY.BackColor = System.Drawing.SystemColors.Control;
            this.panelY.Size = new Size(30, 200);
            this.panelY.SetRoundedShape(10);

            HandleRectangles = new Rectangle[0];

            Components = new Control[]
            {
                this,
                this.displaySizeLabel,
                this.panelX,
                this.panelY
            };
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

            // displaySizeLabel
            displaySizeLabel.Text = $"{Size.Width}x{Size.Height}";
            displaySizeLabel.Location = new Point(this.Location.X, this.Location.Y - displaySizeLabel.Height);
            displaySizeLabel.Refresh();

            // panelX
            panelX.Location = new Point(this.Location.X + this.Width - panelX.Width, this.Location.Y + this.Height);
            panelX.Refresh();

            // panelY
            panelY.Location = new Point(this.Location.X + this.Width, this.Location.Y + this.Height - panelY.Height);
            panelY.Refresh();

        }
    }
}
