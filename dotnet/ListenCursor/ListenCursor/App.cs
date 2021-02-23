using System;
using System.Threading.Tasks;
using static System.Windows.Automation.AutomationElement;

namespace ListenCursor
{
    public class App
    {
        public void Start()
        {
            MouseHook mouseHook = new MouseHook();
            mouseHook.MouseAction += OnMouseAction;
            mouseHook.Start();
           
            //var elem = AutomationElement.FromPoint(new System.Windows.Point(curPos.X, curPos.Y)).Current;
            //var rect = element.Current.BoundingRectangle;
            //Draw(new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
            
            Task.Delay(3600 * 1000).Wait();
        }

        private void OnMouseAction(RawMouseEventArgs e)
        {
            Console.WriteLine("Mouse event {0} at point {1},{2}", e.Message.ToString(), e.Point.x, e.Point.y);
        }

        private void OutToConsole(AutomationElementInformation elem, System.Drawing.Point curPos)
        {
            var str = $"\r'{elem.Name}' '{elem.AutomationId}' '{elem.ControlType.ProgrammaticName}' '{elem.LocalizedControlType}' ({curPos.X}:{curPos.Y})";
            Console.Write($"{str}{new string(' ', Console.BufferWidth - str.Length)}");
        }
    }
}
