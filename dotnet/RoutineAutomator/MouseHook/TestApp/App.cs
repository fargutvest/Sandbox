using MouseHook;
using MouseHook.Hooks;
using System;
using System.Threading;
using MouseEventArgs = MouseHook.MouseEventArgs;

namespace TestApp
{
    public class App
    {
        private ManualResetEvent _mre;
        public void Start()
        {
            var eventHookFactory = new EventHookFactory();
            var mouseWatcher = eventHookFactory.GetMouseWatcher();
            mouseWatcher.Start(MouseMessages.WM_RBUTTONUP, MouseMessages.WM_RBUTTONDOWN);
            mouseWatcher.OnMouseInput += MouseWatcher_OnMouseInput;
            _mre = new ManualResetEvent(false);

            _mre.WaitOne();

        }
        
        private void MouseWatcher_OnMouseInput(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Mouse event {0} at point {1},{2} Thread:{3}", e.Message.ToString(), e.Point.x, e.Point.y, 
                Thread.CurrentThread.ManagedThreadId);
            

            if (e.Message == MouseMessages.WM_RBUTTONUP)
            {
                ShowContextMenu(e.Point.x, e.Point.y);
            }
        }

        private void ShowContextMenu(int x, int y)
        {
            _mre.Set();
        }
    }
}
