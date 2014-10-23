using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();

            bool bCancell = false;


            Task.Factory.StartNew(() =>
            {
                int i = 0;
                Task.WaitAll(tasks.ToArray());
                Console.WriteLine("Task {0} status is now {1}", tasks.ToArray()[0].Id, tasks.ToArray()[0].Status);

                i = 168;
            });

            Task t = Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (!bCancell)
                {
                    Console.Clear();
                    Console.WriteLine(i++);
                }
            }, token);
            tasks.Add(t);

            Console.ReadKey();

            tokenSource.Cancel();
            //bCancell = true;
            Console.WriteLine("Task {0} status is now {1}", t.Id, t.Status);
            
            Console.ReadKey();


        }
    }
}
