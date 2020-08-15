using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adani
{
    public class logObject
    {
        public Logger.Level level;
        public int idThread;
        public string message;
        public DateTime dateTime;
        public logObject() { }
        public logObject(Logger.Level _level, int _idThread, string _message, DateTime _dateTime)
        {
            level = _level;
            idThread = _idThread;
            message = _message;
            dateTime = _dateTime;
        }


    }
}
