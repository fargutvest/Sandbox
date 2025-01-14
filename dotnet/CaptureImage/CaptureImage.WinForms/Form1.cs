using CaptureImage.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CaptureImage.WinForms
{
    public partial class Form1 : Form
    {
        private HotKeysHelper hotKeysHelper;
        private FreezeScreen freezeScreen;
        private BlackoutScreen blackoutScreen;

        private bool isHidden;

        private AppContext appContext;

        public Form1()
        {
            InitializeComponent();
            appContext = new AppContext();
            freezeScreen = new FreezeScreen(appContext);
            blackoutScreen = new BlackoutScreen(appContext);
            hotKeysHelper = new HotKeysHelper();
            hotKeysHelper.RegisterHotKey(Handle, Keys.F6, ShowForm);
            hotKeysHelper.RegisterHotKey(Handle, Keys.Escape, OnEscape);

            isHidden = true;
        }
        protected override void WndProc(ref Message m)
        {
            hotKeysHelper.WndProc(ref m);
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void OnEscape()
        {
            if (isHidden == false)
                HideForm();
            else
                Close();
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
