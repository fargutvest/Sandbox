namespace MouseHook.Hooks
{
    /// <summary>
    /// The mouse messages.
    /// </summary>
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
}