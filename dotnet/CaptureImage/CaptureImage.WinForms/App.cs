using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CaptureImage.Common.Helpers;
using CaptureImage.Common;

namespace CaptureImage.WinForms
{
    internal class App
    {
        internal void Run()
        {
            DescktopInfo info = ScreensHelper.GetDesctopInfo();
            
            FreezeScreen freezeScreen = new FreezeScreen(info.ClientSize, info.Location)
            {
                BackgroundImage = info.Background,
                Region = new Region(info.Path)
            };

            freezeScreen.Show();

            BlackoutScreen blackoutScreen = new BlackoutScreen(info.ClientSize, info.Location)
            {
                BackColor = Color.Black,
                BackgroundImage = BitmapHelper.ChangeOpacity(info.Background, 0.5f),
                TransparencyKey = Color.Red,
                Region = new Region(info.Path)
                //TopMost = true 
            };
            blackoutScreen.Show();

        }
    }
}
