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
        private Thumb.Thumb thumb;
        private DescktopInfo desktopInfo;
        private SelectingTool selectingTool;
        private ITool drawingTool;

        private AppContext appContext;

        public Mode Mode { get; set; }

        public BlackoutScreen(AppContext appContext)
        {
            InitializeComponent();

            this.appContext = appContext;
            desktopInfo = ScreensHelper.GetDesktopInfo();
            ClientSize = desktopInfo.ClientSize;
            Location = desktopInfo.Location;
            BackgroundImage = BitmapHelper.DarkenImage(desktopInfo.Background, 0.5f);
            TransparencyKey = Color.Red;
            Region = new Region(desktopInfo.Path);
            //TopMost = true;

            selectingTool = new SelectingTool();
            selectingTool.Activate();

            appContext.AddControl(this);
            appContext.RefreshDrawingContext();

            drawingTool = new PencilTool(appContext.DrawingContextsKeeper);

            Mode = Mode.Selecting;

            this.thumb = new Thumb.Thumb();
            this.thumb.Size = new Size(0, 0);
            this.thumb.MouseDown += (sender, e) => BlackoutScreen_MouseDown(sender, e.Offset(thumb.Location));
            this.thumb.MouseUp += (sender, e) => BlackoutScreen_MouseUp(sender, e.Offset(thumb.Location));
            this.thumb.MouseMove += (sender, e) => BlackoutScreen_MouseMove(sender, e.Offset(thumb.Location));
            this.thumb.StateChanged += Thumb_StateChanged;
            this.thumb.ActionCalled += Thumb_ActionCalled;
            
            this.Controls.AddRange(thumb.Components);
        }

        public void SwitchToSelectingMode()
        {
            selectingTool.Activate();
            drawingTool.Deactivate();
            Mode = Mode.Selecting;
        }

        private void Thumb_ActionCalled(object sender, Thumb.ThumbAction e)
        {
            switch (e)
            {
                case Thumb.ThumbAction.CopyToClipboard:
                    appContext.MakeScreenshot(selectingTool.selectingRect);
                    SendKeys.Send("{ESC}");
                    break;
                case Thumb.ThumbAction.Undo:
                    appContext.UndoDrawing();
                    if (appContext.DrawingContextsKeeper.DrawingContext.IsClean)
                    {
                        SwitchToSelectingMode();
                    }
                    break;
            }
        }

        private void Thumb_StateChanged(object sender, ThumbState e)
        {
            switch (e)
            {
                case ThumbState.Selecting:
                    selectingTool.Activate();
                    drawingTool.Deactivate();
                    Mode = Mode.Drawing;
                    break;
                case ThumbState.Pencil:
                    selectingTool.Deactivate();
                    drawingTool = new PencilTool(appContext.DrawingContextsKeeper);
                    drawingTool.Activate();
                    Mode = Mode.Drawing;
                    break;
                case ThumbState.Line:
                    selectingTool.Deactivate();
                    drawingTool = new LineTool(appContext.DrawingContextsKeeper);
                    drawingTool.Activate();
                    Mode = Mode.Drawing;
                    break;
                case ThumbState.Arrow:
                    selectingTool.Deactivate();
                    drawingTool = new ArrowTool(appContext.DrawingContextsKeeper);
                    drawingTool.Activate();
                    Mode = Mode.Drawing;
                    break;
                case ThumbState.Rect:
                    selectingTool.Deactivate();
                    drawingTool = new RectTool(appContext.DrawingContextsKeeper);
                    drawingTool.Activate();
                    Mode = Mode.Drawing;
                    break;
            }
        }

        private void BlackoutScreen_MouseMove(object sender, MouseEventArgs e)
        {
            selectingTool.MouseMove(e.Location, this);
            selectingTool.Paint(this.thumb);
            drawingTool.MouseMove(e.Location);
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
