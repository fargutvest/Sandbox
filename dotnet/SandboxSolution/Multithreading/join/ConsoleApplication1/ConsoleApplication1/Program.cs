
using System;
using System.Threading;

class Example
{
    Thread t1;
    Thread t2;
    

     void Main()
    {
         t1 = new Thread(() =>
        {
            Thread.Sleep(4000);
            Console.WriteLine("t1 is ending.");
        });
        t1.Start();
        t1.Join();

        Console.WriteLine("t1.Join() returned.");

         t2 = new Thread(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("t2 is ending.");
        });
        t2.Start();

        

        t2.Join();
        Console.WriteLine("t2.Join() returned.");
        Console.ReadKey();

    }
}