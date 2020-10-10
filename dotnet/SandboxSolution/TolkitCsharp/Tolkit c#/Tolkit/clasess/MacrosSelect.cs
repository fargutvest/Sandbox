using System;
using System.Collections.Generic;
using System.Reflection;


namespace Tolkit
{
    public class MacrosSelect 
    {
        private static readonly Lazy<MacrosSelect> _instance = new Lazy<MacrosSelect>(() => new MacrosSelect());
        public static MacrosSelect Instance => _instance.Value;

        public Dictionary<string, object> Macroses = new Dictionary<string, object>();
        Action<byte[]> SendingMessageDelegate;
        public event Action<byte[]> SendingMessage;


        object _SelectedMacros;
        public object SelectedMacros
        {
            get
            {
                return _SelectedMacros;
            }
        }

        void SendingMessageEventHandler(byte[] bytes)
        {
            if (SendingMessage != null)
                SendingMessage(bytes);
        }

        MacrosSelect()
        {
            try
            {
                //подгрузка библиотеки
                string dllName = "MacrosLibrary.dll";
                string path = string.Format(@"{0}\{1}", Environment.CurrentDirectory, dllName);
                var macrosLibrary = Assembly.LoadFile(path);


                foreach (Type type in macrosLibrary.GetTypes())
                {
                    Type tInterface = type.GetInterface("IMacros");

                    if (tInterface != null)
                    {
                        object ob = type.GetProperty("Instance").GetValue(null, null);
                        Macroses.Add(type.Name, ob);
                    }
                }

                SendingMessageDelegate = SendingMessageEventHandler;
            }
            catch (Exception ex) { }
        }


        public void Select(string key)
        {
            if (_SelectedMacros != null)
                _SelectedMacros.GetType().GetEvent("evSendMessage").RemoveEventHandler(MacrosSelect.Instance._SelectedMacros, SendingMessageDelegate);

            Macroses.TryGetValue(key, out _SelectedMacros);
            _SelectedMacros.GetType().GetEvent("evSendMessage").AddEventHandler(MacrosSelect.Instance._SelectedMacros, SendingMessageDelegate);

        }

        public void ReceiveMessage(byte[] bytes)
        {
            _SelectedMacros.GetType().GetMethod("ReceiveMessage").Invoke(MacrosSelect.Instance._SelectedMacros, new object[] { bytes });
        }

        public bool IsLineTerminator(byte[] bytes)
        {
            bool res = (bool)_SelectedMacros.GetType().GetMethod("IsLineTerminator").Invoke(MacrosSelect.Instance._SelectedMacros, new object[] { bytes });
            return res;
        }

    }
}

