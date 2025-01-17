using CaptureImage.Common.Helpers;
using System.Drawing;
using System.Windows.Forms;
using CaptureImage.Common;
using CaptureImage.Common.Extensions;
using System;
using CaptureImage.WinForms.Properties;

namespace CaptureImage.WinForms.Thumb
{
    public partial class Thumb : UserControl, IThumb
    {
        public Rectangle[] HandleRectangles { get; private set; }

        private Label displaySizeLabel;
        private Panel panelY;
        private Panel panelX;

        private Button btnUndo;
        private Button btnPencil;
        private Button btnLine;
        private Button btnArrow;
        private Button btnRect;

        private Button btnCpClipboard;

        public Control[] Components { get; }

        private ThumbState state;

        public bool IsMouseOver;

        public event EventHandler<ThumbState> StateChanged;

        public event EventHandler<ThumbAction> ActionCalled;

        public Thumb()
        {
            InitializeComponent();

            // displaySizeLabel
            this.displaySizeLabel = new Label();
            this.displaySizeLabel.AutoSize = true;
            this.displaySizeLabel.Font = new Font(displaySizeLabel.Font.FontFamily, 10);
            this.displaySizeLabel.ForeColor = Color.White;
            this.displaySizeLabel.BackColor = Color.Black;

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

            // btnRedo
            this.btnUndo = new Button();
            this.btnUndo.Size = new Size(24, 24);
            this.btnUndo.Location = new Point(3, panelY.Location.Y + panelY.Size.Height - btnUndo.Height - 3);
            this.btnUndo.Text = "U";
            this.btnUndo.MouseClick += (sender, e) => CallAction(ThumbAction.Undo);

            // btnPencil
            this.btnPencil = new Button();
            this.btnPencil.Image = Resources.pencil;
            this.btnPencil.Size = new Size(24, 24);
            this.btnPencil.Location = new Point(3, 3);
            //this.btnPencil.Text = "P";
            this.btnPencil.MouseClick += (sender, e) => SelectState(ThumbState.Pencil);

            // btnLine
            this.btnLine = new Button();
            this.btnLine.Size = new Size(24, 24);
            this.btnLine.Location = new Point(3, 27);
            this.btnLine.Text = "L";
            this.btnLine.MouseClick += (sender, e) => SelectState(ThumbState.Line);

            // btnArrow
            this.btnArrow = new Button();
            this.btnArrow.Size = new Size(24, 24);
            this.btnArrow.Location = new Point(3, 51);
            this.btnArrow.Text = "A";
            this.btnArrow.MouseClick += (sender, e) => SelectState(ThumbState.Arrow);

            // btnRect
            this.btnRect = new Button();
            this.btnRect.Size = new Size(24, 24);
            this.btnRect.Location = new Point(3, 75);
            this.btnRect.Text = "R";
            this.btnRect.MouseClick += (sender, e) => SelectState(ThumbState.Rect);

            // btnCpClipboard
            this.btnCpClipboard = new Button();
            this.btnCpClipboard.Size = new Size(24, 24);
            this.btnCpClipboard.Location = new Point(96, 3);
            this.btnCpClipboard.Text = "C";
            this.btnCpClipboard.MouseClick += (sender, e) => CallAction(ThumbAction.CopyToClipboard);

            this.panelY.Controls.Add(this.btnUndo);
            this.panelY.Controls.Add(this.btnPencil);
            this.panelY.Controls.Add(this.btnLine);
            this.panelY.Controls.Add(this.btnArrow);
            this.panelY.Controls.Add(this.btnRect);

            panelX.Controls.Add(this.btnCpClipboard);

            HandleRectangles = new Rectangle[0];

            Components = new Control[]
            {
                this,
                this.displaySizeLabel,
                this.panelX,
                this.panelY,
            };
        }

        private void SelectState(ThumbState state)
        {
            this.state = state;
            StateChanged?.Invoke(this, state);
        }

        private void CallAction(ThumbAction action)
        {
            ActionCalled?.Invoke(this, action);
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

        public void HideExtra()
        {
            this.panelX.Visible = false;
            this.panelY.Visible = false;
        }

        public void ShowExtra()
        {
            this.panelX.Visible = true;
            this.panelY.Visible = true;
        }

        public void MouseHover(Point point)
        {
            IsMouseOver = true; 
        }

        public void MouseLeave(Point point)
        {
            IsMouseOver = false;
        }
    }
}
