using System;
using System.Runtime.InteropServices;

namespace MouseHook.Hooks
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSLLHOOKSTRUCT
    {
        internal Point pt;
        internal readonly uint mouseData;
        internal readonly uint flags;
        internal readonly uint time;
        internal readonly IntPtr dwExtraInfo;
    }
}