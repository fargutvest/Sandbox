using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Recorder
{
    public static class Draw
    {
        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        private static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);


        public static void Rectangle(Rectangle rect)
        {
            var desktopPtr = GetDC(IntPtr.Zero);
            using (var g = Graphics.FromHdc(desktopPtr))
            {
                var pen = new Pen(new SolidBrush(Color.Yellow));
                g.DrawRectangle(pen, rect);

                g.Dispose();
            }
            ReleaseDC(IntPtr.Zero, desktopPtr);
        }

    }
}
