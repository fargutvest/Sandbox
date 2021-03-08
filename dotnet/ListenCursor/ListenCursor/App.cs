﻿using EventHook;
using EventHook.Helpers;
using EventHook.Hooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using MouseEventArgs = EventHook.MouseEventArgs;

namespace ListenCursor
{
    public class App
    {
        private Queue<ContextMenuResult> _script;
        private ContextMenuForm _menu;
        private AutoResetEvent _areEnd;

        public App()
        {
            _areEnd = new AutoResetEvent(false);
            _script = new Queue<ContextMenuResult>();
        }

        public void Start()
        {
            var eventHookFactory = new EventHookFactory();
            var mouseWatcher = eventHookFactory.GetMouseWatcher();
            mouseWatcher.Start(MouseMessages.WM_RBUTTONUP, MouseMessages.WM_RBUTTONDOWN);
            mouseWatcher.OnMouseInput += MouseWatcher_OnMouseInput;


            _areEnd.WaitOne();
            var fileName = "script.txt";
            File.WriteAllText(fileName, _script.ToJson());
            Process.Start(fileName);
        }

        private bool _mrbDownPressed;

        private void MouseWatcher_OnMouseInput(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Mouse event {0} at point {1},{2}", e.Message.ToString(), e.Point.x, e.Point.y);

            _mrbDownPressed = e.Message == MouseMessages.WM_RBUTTONDOWN ? true : false;

            if (e.Message == MouseMessages.WM_RBUTTONUP)
            {
                _mrbDownPressed = false;
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
                var dialogResult = _menu.ShowDialog();
                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    _script.Enqueue(_menu.Result);
                }
                else if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    _areEnd.Set();
                }
            }, CancellationToken.None,
                        TaskCreationOptions.None,
                        new SyncFactory().GetTaskScheduler());
        }

       

      
    }
}
