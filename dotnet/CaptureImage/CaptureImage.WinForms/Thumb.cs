﻿using CaptureImage.Common.Helpers;
using System.Drawing;
using System.Windows.Forms;
using CaptureImage.Common;
using CaptureImage.Common.Extensions;
using System;

namespace CaptureImage.WinForms
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
        private Button btnRect;

        public Control[] Components { get; }

        private ThumbState state;

        public bool IsMouseOver;

        public event EventHandler<ThumbState> StateChanged;

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

            // btnRedo
            this.btnUndo = new Button();
            this.btnUndo.Size = new Size(24, 24);
            this.btnUndo.Location = new Point(3, panelY.Location.Y + panelY.Size.Height - btnUndo.Height - 3);
            this.btnUndo.Text = "U";
            this.btnUndo.MouseClick += BtnUndo_MouseClick;

            // btnPencil
            this.btnPencil = new Button();
            this.btnPencil.Size = new Size(24, 24);
            this.btnPencil.Location = new Point(3, 3);
            this.btnPencil.Text = "P";
            this.btnPencil.MouseClick += BtnPencil_MouseClick;

            // btnLine
            this.btnLine = new Button();
            this.btnLine.Size = new Size(24, 24);
            this.btnLine.Location = new Point(3, 27);
            this.btnLine.Text = "L";
            this.btnLine.MouseClick += BtnLine_MouseClick;

            // btnRect
            this.btnRect = new Button();
            this.btnRect.Size = new Size(24, 24);
            this.btnRect.Location = new Point(3, 51);
            this.btnRect.Text = "R";
            this.btnRect.MouseClick += BtnRect_MouseClick;

            this.panelY.Controls.Add(this.btnUndo);
            this.panelY.Controls.Add(this.btnPencil);
            this.panelY.Controls.Add(this.btnLine);
            this.panelY.Controls.Add(this.btnRect);

            HandleRectangles = new Rectangle[0];

            Components = new Control[]
            {
                this,
                this.displaySizeLabel,
                this.panelX,
                this.panelY,
            };
        }

        private void BtnPencil_MouseClick(object sender, MouseEventArgs e)
        {
            state = ThumbState.Pencil;
            StateChanged?.Invoke(this, state);
        }


        private void BtnLine_MouseClick(object sender, MouseEventArgs e)
        {
            state = ThumbState.Line;
            StateChanged?.Invoke(this, state);
        }

        private void BtnRect_MouseClick(object sender, MouseEventArgs e)
        {
            state = ThumbState.Rect;
            StateChanged?.Invoke(this, state);
        }

        private void BtnUndo_MouseClick(object sender, MouseEventArgs e)
        {
            state = ThumbState.Selecting;
            StateChanged?.Invoke(this, state);
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
