using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ListenCursor
{
    public class App
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);


        private CancellationToken _token;


        public App(CancellationToken token)
        {
            _token = token;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                while (_token.IsCancellationRequested == false)
                {
                    try
                    {
                        var curPos = Cursor.Position;
                        var elem = AutomationElement.FromPoint(new System.Windows.Point(curPos.X, curPos.Y)).Current;
                        //var rect = element.Current.BoundingRectangle;
                        //Draw(new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));


                        var str = $"\r'{elem.Name}' '{elem.AutomationId}' '{elem.ControlType.ProgrammaticName}' '{elem.LocalizedControlType}' ({curPos.X}:{curPos.Y})";
                        Console.Write($"{str}{new string(' ', Console.BufferWidth - str.Length)}");
                    }
                    catch (Exception ex)
                    {
                        var str = "error";
                        Console.Write($"\r{str}{Console.BufferWidth - str.Length}");
                    }
                   
                }
            });
        }

        //private void Draw(Rectangle rect)
        //{
        //    var desktopPtr = GetDC(IntPtr.Zero);
        //    using (var g = Graphics.FromHdc(desktopPtr))
        //    {
        //        var pen = new Pen(new SolidBrush(Color.Yellow));
        //        g.DrawRectangle(pen, rect);

        //        g.Dispose();
        //    }
        //    ReleaseDC(IntPtr.Zero, desktopPtr);
        //}
    }
}
