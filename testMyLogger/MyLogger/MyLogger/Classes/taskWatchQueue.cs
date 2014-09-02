using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Adani
{
    class taskWatchQueue : IDisposable
    {
        Task task;
        AutoResetEvent areQueued;
        AutoResetEvent areCancel;
        CancellationTokenSource cts;


        public taskWatchQueue()
        {
            areQueued = new AutoResetEvent(false);
            areCancel = new AutoResetEvent(false);
            cts = new CancellationTokenSource();
            task = new Task(Execute, cts.Token);
            task.ContinueWith(continuation);
            task.Start();
        }

        void continuation(Task antecendet)
        {
            antecendet.Dispose();
            antecendet = null;
        }

        void Execute(object cancel)
        {
            CancellationToken ct = (CancellationToken)cancel;
            try
            {
                while (true)
                {
                    if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                    if (Logger.Instance._queue.Count > 0)
                    {
                        Logger.Instance.WriteLine(Logger.Instance.Dequeue());
                    }

                    switch (WaitHandle.WaitAny(new WaitHandle[] { areQueued, areCancel }))
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                    }
                }
            }
            catch (OperationCanceledException ex) { }
            catch (Exception ex) { }
        }

        public void areQueuedSet()
        {
            areQueued.Set();
        }
        public void Dispose()
        {

        }
    }
}
