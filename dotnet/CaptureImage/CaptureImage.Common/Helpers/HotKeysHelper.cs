using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CaptureImage.Common.Helpers
{
    public class HotKeysHelper
    {
        private Dictionary<Keys, Action> hotKeyActions = new Dictionary<Keys, Action>();

        private Dictionary<int, Keys> hotKeyIds = new Dictionary<int, Keys>();

        public void RegisterHotKey(IntPtr handle, Keys hotKey, Action action)
        {
            int hotKeyId = (int)hotKey;
            hotKeyIds.Add(hotKeyId, hotKey);
            hotKeyActions.Add(hotKey, action);
            WinApi.RegisterHotKey(handle, hotKeyId, 0, (int)hotKey);
        }


        public void WndProc(ref Message m)
        {
            int wParam = m.WParam.ToInt32();

            if (m.Msg == 0x0312 && hotKeyIds.ContainsKey(wParam))
            {
                Keys hotKey = hotKeyIds[wParam];
                if (hotKeyActions.ContainsKey(hotKey))
                {
                    hotKeyActions[hotKey].Invoke();
                }   
            }
        }
    }
}
