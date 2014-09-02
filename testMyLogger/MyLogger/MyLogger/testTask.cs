using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Adani
{
    class testTask : IDisposable
    {
        Task task;
        AutoResetEvent areQueued;
        AutoResetEvent areCancel;
        CancellationTokenSource cts;
        Random rand;
        string[] arr = new string[] { "parpa pam pam pararam pam", "op op op op op" };

        public testTask()
        {
            areQueued = new AutoResetEvent(false);
            areCancel = new AutoResetEvent(false);
            cts = new CancellationTokenSource();
            rand = new Random();
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
            int f = 0;
            CancellationToken ct = (CancellationToken)cancel;
            try
            {
                while (true)
                {
                    areQueued.Set();
                    if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                    f = rand.Next(0,2);
                    Logger.Instance.Info(arr[f]);
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
