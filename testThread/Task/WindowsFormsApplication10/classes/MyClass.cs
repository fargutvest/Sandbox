using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApplication10
{
    delegate void SetTextCallback(string text);
    class MyClass : IDisposable
    {
        public Task MyTask;
        public CancellationTokenSource cts;
        MyForm refMyForm;
        public MyClass(MyForm _myForm) //constructor
        {
            refMyForm = _myForm;
        }

        
        public void StopTask(Task _task, CancellationTokenSource _cts )
        {

            if (_task != null)
            {
                _cts.Cancel();

                while (_task.Wait(10) == false)
                {
                    Application.DoEvents();
                }

                //  SetTextSafe("OK...");
                //   SetText("");

                _task = null;
            }
        }

        public void StartTask()
        {
            StopTask(MyTask, cts);

            refMyForm.deleteFileLog();

            cts = new CancellationTokenSource();
            MyTask = new Task(TaskProcSafe, cts.Token);
            /*Task t = _task.ContinueWith((antecedent) =>
            {
                if (cts.IsCancellationRequested)
                    refMyForm.SetTextSafe("IsCancellationRequested");
                refMyForm.SetTextSafe(string.Format("OK... {0}", antecedent.Status));
                //SetText("");
            }, TaskScheduler.FromCurrentSynchronizationContext());*/

            refMyForm.SetTextSafe("GO...");
            MyTask.Start();
            //_task = t;
        }


        private void TaskProcSafe(object cancell)
        {
            CancellationToken cancellationToken = (CancellationToken)cancell;
            refMyForm.SetTextSafe("Start...");
            try
            {
                int i = 0;
                while (i < 10)
                {
                    refMyForm.SetTextSafe(i.ToString());
                    i++;
                    Task.Delay(100, cancellationToken).Wait();
                    cancellationToken.ThrowIfCancellationRequested();
                    //if (cancell.IsCancellationRequested)
                    //throw new OperationCanceledException(cancellationToken);
                }
            }
            catch (Exception)
            {
                refMyForm.SetTextSafe("terminate");
            }

            refMyForm.SetTextSafe("stop");
        }

     

        public void Dispose()
        {
            StopTask(MyTask, cts);
        }
    }
}
