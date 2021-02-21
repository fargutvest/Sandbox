using System.Threading;
using System.Threading.Tasks;

namespace ListenCursor
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var app = new App(cts.Token);
            app.Start();
            Task.Delay(3600 * 1000).Wait();
            cts.Cancel();
        }
    }
}
