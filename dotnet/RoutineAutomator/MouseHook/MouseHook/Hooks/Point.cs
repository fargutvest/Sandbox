using System.Runtime.InteropServices;

namespace MouseHook.Hooks
{
    /// <summary>
    /// The point co-ordinate.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public readonly int x;
        public readonly int y;
    }
}