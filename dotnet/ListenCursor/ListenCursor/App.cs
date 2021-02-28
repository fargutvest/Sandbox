using EventHook;
using EventHook.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using MouseEventArgs = EventHook.MouseEventArgs;

namespace ListenCursor
{
    public class App
    {

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
                var contextMenu = new Form();
                contextMenu.Width = 600;
                var ui = AutomationElement.FromPoint(new System.Windows.Point(x, y));
                contextMenu.Controls.Add(new Label() { Text = $"{ui.Current.Name}", Width = contextMenu.Width, Height = 20, Location = new System.Drawing.Point(0,0)});
                var supportedPatterns = ui.GetSupportedPatterns();
                var counter = 0;
                foreach (var item in supportedPatterns)
                {
                    counter++;
                    contextMenu.Controls.Add(new Label() { Text = $"{item.GetType().Name}: {item.ProgrammaticName}", Width = contextMenu.Width, Height = 20, Location = new System.Drawing.Point(0, 20 * counter) });
                }
                
                contextMenu.StartPosition = FormStartPosition.Manual;
                contextMenu.Location = new System.Drawing.Point(x, y);
                contextMenu.Show();
                contextMenu.TopMost = true;
                contextMenu.BringToFront();
            }, CancellationToken.None,
                        TaskCreationOptions.None,
                        new SyncFactory().GetTaskScheduler());
        }
    }
}
