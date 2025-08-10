using System.Windows.Forms;

namespace MouseHook.Helpers
{
    /// <summary>
    /// A dummy class to create a dummy invisible window object
    /// </summary>
    internal sealed class MessageHandler : NativeWindow
    {
        internal MessageHandler()
        {
            CreateHandle(new CreateParams());
        }

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
        }
    }
}