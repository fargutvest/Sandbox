using System;
using System.Windows.Forms;

namespace CaptureImage.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FreezeScreen freezeScreen = new FreezeScreen();
            freezeScreen.Show();

            BlackoutScreen blackoutScreen = new BlackoutScreen();
            blackoutScreen.Show();
        }
    }
}
