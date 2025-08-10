using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;


namespace MouseHook.Hooks
{
    /// <summary>
    /// http://stackoverflow.com/questions/11607133/global-mouse-event-handler
    /// </summary>
    internal class MouseHook
    {
        private const int WH_MOUSE_LL = 14;
        private static IntPtr _hookId = IntPtr.Zero;

        private readonly User32.LowLevelMouseProc Proc;

        private MouseMessages[] _mouseMessagesToSuppress;

        internal MouseHook()
        {
            Proc = HookCallback;
        }

        internal event EventHandler<RawMouseEventArgs> MouseAction = delegate { };

        /// <summary>
        /// This needs to run on UI thread context
        /// So use task factory with the shared UI message pump thread
        /// </summary>
        /// <param name="mouseMessagesToSuppress"></param>
        internal void Start(params MouseMessages[] mouseMessagesToSuppress)
        {
            _mouseMessagesToSuppress = mouseMessagesToSuppress;
            _hookId = SetHook(Proc);
        }

        internal void Stop()
        {
            User32.UnhookWindowsHookEx(_hookId);
        }

        private static IntPtr SetHook(User32.LowLevelMouseProc proc)
        {
            var hook = User32.SetWindowsHookEx(WH_MOUSE_LL, proc, User32.GetModuleHandle("user32"), 0);
            if (hook == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            return hook;
        }

        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return User32.CallNextHookEx(_hookId, nCode, wParam, lParam);
            }

            var hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

            MouseAction(null, new RawMouseEventArgs { Message = (MouseMessages)wParam, Point = hookStruct.pt, MouseData = hookStruct.mouseData });

            var result = User32.CallNextHookEx(_hookId, nCode, wParam, lParam);

            if (_mouseMessagesToSuppress!= null && _mouseMessagesToSuppress.Any(msg=> msg == (MouseMessages)wParam))
            {
                return new IntPtr(1);
            }

            return result;
        }
    }
}
