using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using EventHook;

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

        private void Draw(Rectangle rect)
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

        public void TestHook()
        {
            using (var eventHookFactory = new EventHookFactory())
            {
                var keyboardWatcher = eventHookFactory.GetKeyboardWatcher();
                keyboardWatcher.Start();
                keyboardWatcher.OnKeyInput += (s, e) =>
                {
                    Console.WriteLine(string.Format("Key {0} event of key {1}", e.KeyData.EventType, e.KeyData.Keyname));
                };

                var mouseWatcher = eventHookFactory.GetMouseWatcher();
                mouseWatcher.Start();
                mouseWatcher.OnMouseInput += (s, e) =>
                {
                    Console.WriteLine(string.Format("Mouse event {0} at point {1},{2}", e.Message.ToString(), e.Point.x, e.Point.y));
                };

                var clipboardWatcher = eventHookFactory.GetClipboardWatcher();
                clipboardWatcher.Start();
                clipboardWatcher.OnClipboardModified += (s, e) =>
                {
                    Console.WriteLine(string.Format("Clipboard updated with data '{0}' of format {1}", e.Data, e.DataFormat.ToString()));
                };


                var applicationWatcher = eventHookFactory.GetApplicationWatcher();
                applicationWatcher.Start();
                applicationWatcher.OnApplicationWindowChange += (s, e) =>
                {
                    Console.WriteLine(string.Format("Application window of '{0}' with the title '{1}' was {2}", e.ApplicationData.AppName, e.ApplicationData.AppTitle, e.Event));
                };

                var printWatcher = eventHookFactory.GetPrintWatcher();
                printWatcher.Start();
                printWatcher.OnPrintEvent += (s, e) =>
                {
                    Console.WriteLine(string.Format("Printer '{0}' currently printing {1} pages.", e.EventData.PrinterName, e.EventData.Pages));
                };

                //waiting here to keep this thread running           
                Console.Read();

                //stop watching
                keyboardWatcher.Stop();
                mouseWatcher.Stop();
                clipboardWatcher.Stop();
                applicationWatcher.Stop();
                printWatcher.Stop();
            }
        }

    }
}
