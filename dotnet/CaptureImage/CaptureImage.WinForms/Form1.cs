using CaptureImage.Common;
using CaptureImage.Common.Helpers;
using System;
using System.Windows.Forms;

namespace CaptureImage.WinForms
{
    public partial class Form1 : Form
    {
        private HotKeysHelper hotKeysHelper;
        private FreezeScreen freezeScreen;
        private BlackoutScreen blackoutScreen;

        public Form1()
        {
            InitializeComponent();
            freezeScreen = new FreezeScreen();
            blackoutScreen = new BlackoutScreen();
            hotKeysHelper = new HotKeysHelper();
            hotKeysHelper.RegisterHotKey(Handle, Keys.F6, ShowForm);
            hotKeysHelper.RegisterHotKey(Handle, Keys.Escape, HideForm);
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

        private void HideForm()
        {
            freezeScreen.Hide();
            blackoutScreen.Hide();
        }

        private void ShowForm()
        {
            freezeScreen.Show();
            blackoutScreen.Show();
        }
    }
}
