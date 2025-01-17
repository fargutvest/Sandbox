using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CaptureImage.WinForms.Properties;

namespace CaptureImage.WinForms
{
    public class AppContext : ApplicationContext
    {
        private List<Control> controls;
        private Control canvas;
        private NotifyIcon trayIcon;
        public HotKeysHelper hotKeysHelper;
        private FreezeScreen freezeScreen;
        private BlackoutScreen blackoutScreen;
        private bool isHidden;

        public DrawingContextsKeeper DrawingContextsKeeper { get; private set; }

        public AppContext()
        {
            isHidden = true;
            this.controls = new List<Control>();

            MainForm = new MainForm(this);

            freezeScreen = new FreezeScreen(this);
            blackoutScreen = new BlackoutScreen(this);
            hotKeysHelper = new HotKeysHelper();

            hotKeysHelper.RegisterHotKey(MainForm.Handle, Keys.F6, ShowForm);
            hotKeysHelper.RegisterHotKey(MainForm.Handle, Keys.Escape, OnEscape);

            trayIcon = new NotifyIcon()
            {
                Icon = Resources.ashot,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Сделать скриншот", Show),
                    new MenuItem("Выход", Exit)})
            };

            trayIcon.Visible = true;

        }

        public void AddControl(Control control, bool isCanvas = false)
        {
            if (isCanvas)
                canvas = control;

            controls.Add(control);
        }

        public void RefreshDrawingContext()
        {
            DrawingContextsKeeper = new DrawingContextsKeeper();

            Image[] canvasImages = new Image[controls.Count];

            for (int i = 0; i < controls.Count; i++)
               canvasImages[i] = controls[i].BackgroundImage;

            DrawingContext drawingContext = DrawingContext.Create(canvasImages, controls.ToArray(), isClean: true);

            DrawingContextsKeeper.DrawingContext = drawingContext;
            
            DrawingContextsKeeper.SaveContext();
        }

        public void MakeScreenshot(Rectangle rect) 
        {
            Clipboard.SetImage(BitmapHelper.Crop((Bitmap)canvas.BackgroundImage, rect));
        }

        public void UndoDrawing()
        {
            DrawingContextsKeeper.RevertToPreviousContext();

            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].BackgroundImage = DrawingContextsKeeper.DrawingContext.CanvasImages[i];
                controls[i].Invalidate();
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void Show(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void OnEscape()
        {
            if (blackoutScreen.Mode == Mode.Drawing)
            {
                blackoutScreen.SwitchToSelectingMode();
                return;
            }
            else if (blackoutScreen.Mode == Mode.Selecting)
            {
                if (isHidden == false)
                    HideForm();
                else
                    Application.Exit();
            }
        }
        private void HideForm()
        {
            freezeScreen.Hide();
            blackoutScreen.Hide();
            isHidden = true;
        }

        private void ShowForm()
        {
            freezeScreen.Show();
            blackoutScreen.Show();
            isHidden = false;
        }
    }
}
    