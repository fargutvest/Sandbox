
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tolkit
{
    public interface IMacros
    {
        void ReceiveMessage(byte[] bytes);
        event Action<byte[]> evSendMessage;
        bool IsLineTerminator(byte[] bytes);
    }
}
