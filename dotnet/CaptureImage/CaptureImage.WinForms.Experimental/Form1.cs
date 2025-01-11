using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using System;
using System.Windows.Forms;
using CaptureImage.Common.Extensions;
using CaptureImage.Common.Tools;

namespace CaptureImage.WinForms.Experimental
{
    public partial class Form1 : Form
    {
        private bool isInit = true;

        private DescktopInfo desktopInfo;

        private HotKeysHelper hotKeysHelper;

        private SelectingTool selectingTool;

        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
             ControlStyles.UserPaint |
             ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            desktopInfo = ScreensHelper.GetDesktopInfo();
            selectingTool = new SelectingTool();
            hotKeysHelper = new HotKeysHelper();
            hotKeysHelper.RegisterHotKey(Handle, Keys.F6, ShowForm);
            hotKeysHelper.RegisterHotKey(Handle, Keys.Escape, HideForm);

            // TODO: Что быстрее работает ? Задать поместить скриншот фоном формы один раз в конструкторе, или каждый раз рисовать скриншот при перерисовке формы в методе Paint ?
            //BackgroundImage = desktopInfo.Background;
        }

        protected override void WndProc(ref Message m)
        {
            hotKeysHelper.WndProc(ref m);
            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer
            {
                Interval = 10
            };
            timer.Tick += new EventHandler(MouseMoveEvent);
            timer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (isInit)
            {
                HideForm();
                isInit = false;
            }

            // TODO: Что быстрее работает ? Задать поместить скриншот фоном формы один раз в конструкторе, или каждый раз рисовать скриншот при перерисовке формы в методе Paint ?
            e.Graphics.DrawImage(desktopInfo.Background, desktopInfo.BackgroundRect, desktopInfo.BackgroundRect, opacity: 0.5f);
            selectingTool.Paint(e.Graphics, desktopInfo.Background);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            selectingTool.MouseUp(this.GetMousePosition());
            Refresh();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            selectingTool.MouseDown(e.Location);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                selectingTool.Select(desktopInfo.BackgroundRect);
                Refresh();
            }
        }


        private void MouseMoveEvent(object sender, EventArgs e)
        {
            selectingTool.MouseMove(this.GetMousePosition(), this);
            Refresh();
        }

        private void HideForm()
        {
            Visible = false;
            selectingTool.HideSelecting();
        }

        private void ShowForm()
        {
            Size = desktopInfo.Bounds.Size;
            Location = desktopInfo.Location;
            Visible = true;
        }
    }
}
