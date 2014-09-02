using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Adani
{
    public class Logger : IDisposable
    {
        public string pathLogFile = null;
        const string pathDefault = "log.txt";
        public Queue<logObject> _queue;
        static Logger _logger;
        StreamReader reader;
        StreamWriter writer;
        logObject lastLogObject;
        taskWatchQueue _taskWatchQueue;
        long position;
        int duplicateCount = 1;

        public enum Level
        {
            Info,
            Warning,
            Error,
            Fatal
        }

        static public Logger Instance
        {
            get { return _logger ?? (_logger = new Logger()); }
        }

        protected Logger()
        {
            _queue = new Queue<logObject>();
            if (pathLogFile == null)
                pathLogFile = pathDefault;

            _taskWatchQueue = new taskWatchQueue();
        }

        public void Info(string s)
        {
            Enqueue(new logObject(Level.Info, Thread.CurrentThread.ManagedThreadId, s, DateTime.Now));
        }
        public void Warning(string s)
        {
            Enqueue(new logObject(Level.Warning, Thread.CurrentThread.ManagedThreadId, s, DateTime.Now));
        }

        public void Error(string s)
        {
            Enqueue(new logObject(Level.Error, Thread.CurrentThread.ManagedThreadId, s, DateTime.Now));
        }

        public void Fatal(string s)
        {

        }

        bool checkDuplicate(logObject lo)
        {
            return lastLogObject == null ? false : lastLogObject.message == lo.message & lastLogObject.level == lo.level;
        }
        public void WriteLine(logObject lo)
        {
            if (checkDuplicate(lo))
            {
                ReWriteLine(lo);
            }
            else
            {
                duplicateCount = 1;
                using (writer = File.AppendText(pathLogFile))
                {
                    string g = Formatter(lo);
                    writer.WriteLine(g);
                    position = writer.BaseStream.Position;
                }
            }
            lastLogObject = lo;
        }


        bool ReWriteLine(logObject lo)
        {
            duplicateCount += 1;
            using (writer = new StreamWriter(pathLogFile,true))
            {
                writer.BaseStream.Position = position;
                string g = Formatter(lo);
                writer.WriteLine(g);
            }
            return true;
        }


        public void Enqueue(logObject lo)
        {
            lock (_queue)
            {
                _queue.Enqueue(lo);
            }
            _taskWatchQueue.areQueuedSet();
        }

        public logObject Dequeue()
        {
            lock (_queue)
            {
                return _queue.Dequeue();
            }
        }

        string Formatter(logObject lo)
        {
            string count = duplicateCount > 1 ? string.Format("({0})", duplicateCount.ToString()) : "";
            return string.Format("{0: dd:mm:yy hh:mm:ss:ms} [thread: {1}] [{2}] {3} {4}", lo.dateTime, lo.idThread, lo.level.ToString(), lo.message, count);
        }

        public void Dispose()
        {

        }
    }
}
