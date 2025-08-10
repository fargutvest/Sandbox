using System;
using MouseHook.Hooks;

namespace MouseHook
{
    /// <summary>
    ///     Event argument to pass data through user callbacks.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        public MouseMessages Message { get; set; }
        public Point Point { get; set; }
        public uint MouseData { get; set; }
    }
}