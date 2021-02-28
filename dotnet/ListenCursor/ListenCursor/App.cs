using EventHook;
using EventHook.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using MouseEventArgs = EventHook.MouseEventArgs;

namespace ListenCursor
{
    public class App
    {
        private ContextMenuForm _menu;

        public App()
        {
        }

        public void Start()
        {
            var eventHookFactory = new EventHookFactory();
            var mouseWatcher = eventHookFactory.GetMouseWatcher();
            mouseWatcher.Start();
            mouseWatcher.OnMouseInput += MouseWatcher_OnMouseInput;
            Console.Read();
        }

        private void MouseWatcher_OnMouseInput(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Mouse event {0} at point {1},{2}", e.Message.ToString(), e.Point.x, e.Point.y);

            if (e.Message == EventHook.Hooks.MouseMessages.WM_RBUTTONDOWN)
            {
                ShowContextMenu(e.Point.x, e.Point.y);
            }
        }


        private void OutToConsole(AutomationElement.AutomationElementInformation elem, int x, int y)
        {
            var str = $"\r'{elem.Name}' '{elem.AutomationId}' '{elem.ControlType.ProgrammaticName}' '{elem.LocalizedControlType}' ({x}:{y})";
            Console.Write($"\r{str}");
        }

        private void ShowContextMenu(int x, int y)
        {
            Task.Factory.StartNew(() =>
            {
                _menu = new ContextMenuForm() { TopMost = true, Top = 99 };
                _menu.Fill(x, y);
                var result = _menu.ShowDialog();
                var r = result;
                var rr = _menu.Result;

            }, CancellationToken.None,
                        TaskCreationOptions.None,
                        new SyncFactory().GetTaskScheduler());
        }

       

      
    }
}
