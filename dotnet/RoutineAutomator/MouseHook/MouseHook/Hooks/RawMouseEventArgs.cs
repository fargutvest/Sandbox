using System;

namespace MouseHook.Hooks
{
    internal class RawMouseEventArgs : EventArgs
    {
        internal MouseMessages Message { get; set; }
        internal Point Point { get; set; }
        internal uint MouseData { get; set; }
    }
}