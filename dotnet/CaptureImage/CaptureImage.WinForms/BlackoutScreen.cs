using CaptureImage.Common;
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

        private DrawingTool drawingTool;

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

            drawingTool = new DrawingTool();

            this.thumb = new Thumb();
            this.thumb.Size = new Size(0,0);
            this.thumb.MouseDown += (sender, e) => BlackoutScreen_MouseDown(sender, e.Offset(thumb.Location));
            this.thumb.MouseUp += (sender, e) => BlackoutScreen_MouseUp(sender, e.Offset(thumb.Location));
            this.thumb.MouseMove += (sender, e) => BlackoutScreen_MouseMove(sender, e.Offset(thumb.Location));
            
            this.Controls.AddRange(thumb.Components);
        }

        private void BlackoutScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (thumb.ThumbState == ThumbState.Selecting)
            {
                selectingTool.MouseMove(e.Location, this);
                selectingTool.Pulse(this.thumb);
            }

            if (thumb.ThumbState == ThumbState.Drawing)
            {
                drawingTool.MouseMove(this.CreateGraphics(), e.Location);

                if (selectingTool.IsMouseOver(e.Location))
                {
                    Point pointInSelection = selectingTool.Translate(e.Location);

                    if (thumb.IsMouseOver == false)
                        thumb.MouseHover(pointInSelection);
                    
                    thumb.OnMouseMove(pointInSelection);
                }
                else
                {
                    if (thumb.IsMouseOver)
                        thumb.MouseLeave(e.Location);   
                }
            }
        }

        private void BlackoutScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (thumb.ThumbState == ThumbState.Selecting)
                    selectingTool.MouseUp(e.Location);

                if (thumb.ThumbState == ThumbState.Drawing)
                    drawingTool.MouseUp(e.Location);
            }
        }

        private void BlackoutScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (thumb.ThumbState == ThumbState.Selecting)
                    selectingTool.MouseDown(e.Location);

                if (thumb.ThumbState == ThumbState.Drawing)
                    drawingTool.MouseDown(e.Location);
            }
        }

        private void BlackoutScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                if (thumb.ThumbState == ThumbState.Selecting)
                    selectingTool.Select(desktopInfo.BackgroundRect);
            }
        }
    }
}
