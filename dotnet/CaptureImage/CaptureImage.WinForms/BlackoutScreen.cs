﻿using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using CaptureImage.Common.Tools;
using System.Drawing;
using System.Windows.Forms;
using CaptureImage.Common.Extensions;
namespace CaptureImage.WinForms
{
    public partial class BlackoutScreen : ScreenBase
    {
        private bool isInit = true;
        private Thumb thumb;
        private DescktopInfo desktopInfo;
        private SelectingTool selectingTool;
        private ITool drawingTool;

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
            //TopMost = true;

            selectingTool = new SelectingTool();
            selectingTool.MouseEnterSelection += SelectingTool_MouseEnterSelection;
            selectingTool.Activate();

            drawingTool = new PencilTool();

            this.thumb = new Thumb();
            this.thumb.Size = new Size(0,0);
            this.thumb.MouseDown += (sender, e) => BlackoutScreen_MouseDown(sender, e.Offset(thumb.Location));
            this.thumb.MouseUp += (sender, e) => BlackoutScreen_MouseUp(sender, e.Offset(thumb.Location));
            this.thumb.MouseMove += (sender, e) => BlackoutScreen_MouseMove(sender, e.Offset(thumb.Location));
            this.thumb.StateChanged += Thumb_StateChanged;
            
            this.Controls.AddRange(thumb.Components);
        }

        private void SelectingTool_MouseEnterSelection(object sender, Point point)
        {
            drawingTool.MouseHoverControl(selectingTool.Translate(point));
        }

        private void Thumb_StateChanged(object sender, ThumbState e)
        {
            switch (e)
            {
                case ThumbState.Selecting:
                    selectingTool.Activate();
                    drawingTool.Deactivate();
                    break;
                case ThumbState.Pencil:
                    selectingTool.Deactivate();
                    drawingTool = new PencilTool();
                    drawingTool.Activate();
                    break;
                case ThumbState.Line:
                    selectingTool.Deactivate();
                    drawingTool = new LineTool();
                    drawingTool.Activate();
                    break;
            }
        }

        private void BlackoutScreen_MouseMove(object sender, MouseEventArgs e)
        {
            selectingTool.MouseMove(e.Location, this);
            selectingTool.Paint(this.thumb);

            drawingTool.MouseMove(this.CreateGraphics(), e.Location);

            if (selectingTool.IsMouseOver)
                drawingTool.MouseMove(thumb, selectingTool.Translate(e.Location));
            
        }

        private void BlackoutScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectingTool.MouseUp(e.Location);
                drawingTool.MouseUp();
            }
        }

        private void BlackoutScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectingTool.MouseDown(e.Location);

                if (sender is Thumb)
                    drawingTool.MouseDown(selectingTool.Translate( e.Location), onControl: true);
                
                    drawingTool.MouseDown(e.Location);
            }
        }

        private void BlackoutScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                selectingTool.Select(desktopInfo.BackgroundRect);
            }
        }
    }
}
