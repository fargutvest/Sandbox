using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ListenCursor
{
    internal class RawMouseEventArgs : EventArgs
    {
        internal MouseMessages Message { get; set; }
        internal Point Point { get; set; }
        internal uint MouseData { get; set; }
    }

    public enum MouseMessages
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEWHEEL = 0x020A,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_WHEELBUTTONDOWN = 0x207,
        WM_WHEELBUTTONUP = 0x208,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public readonly int x;
        public readonly int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MSLLHOOKSTRUCT
    {
        internal Point pt;
        internal readonly uint mouseData;
        internal readonly uint flags;
        internal readonly uint time;
        internal readonly IntPtr dwExtraInfo;
    }

    internal class MouseHook
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
           LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);


        private static IntPtr _hookId = IntPtr.Zero;

        public Action<RawMouseEventArgs> MouseAction;


        internal void Start()
        {
            Task.Factory.StartNew(() =>
            {
                _hookId = SetHook(HookCallback);
            },
              CancellationToken.None,
              TaskCreationOptions.None,
              GetScheduler()).Wait();


        }

        private TaskScheduler GetScheduler()
        {

            TaskScheduler current = null;

            new Task(() =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(
                    new Action(() =>
                    {
                        Volatile.Write(ref current, TaskScheduler.FromCurrentSynchronizationContext());
                    }), DispatcherPriority.Normal);
                Dispatcher.Run();
            }).Start();


            while (Volatile.Read(ref current) == null)
            {
                Thread.Sleep(10);
            }

            return Volatile.Read(ref current);

        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            var hook = SetWindowsHookEx(14, proc, GetModuleHandle("user32"), 0);
            if (hook == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            return hook;
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            MSLLHOOKSTRUCT hookStruct;
            hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            MouseAction?.Invoke(new RawMouseEventArgs { Message = (MouseMessages)wParam, Point = hookStruct.pt, MouseData = hookStruct.mouseData });
            return IntPtr.Zero;
        }
    }
}
