using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Generator
    {
        public event Action evAutoSearch;
        public event Action<string> evCheckPort;
        public event Action evConnected;

        System.Threading.AutoResetEvent AreSelectTypeGen = new System.Threading.AutoResetEvent(false);

        public Generator()
        {

        }

        public void SetSelectTypeGen()
        {
            AreSelectTypeGen.Set();
        }

        public bool SelectGenerator()
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                AreSelectTypeGen.WaitOne();

                string[] array = new string[] { 
                "com1", "com2", "com3", "com4", "com5", 
                "com6", "com7", "com8", "com9", "com10" };

                foreach (string portName in array)
                {
                    if (evCheckPort != null)
                        evCheckPort(portName);

                    //check ...

                    if (portName == "com9")
                    {
                        if (evConnected != null)
                            evConnected();
                        break;
                    }
                    System.Threading.Thread.Sleep(1000);
                    

                }
            });

            if (evAutoSearch != null)
                evAutoSearch();

            int t = 0;
            int t1 = 0;
            int t2 = 0;
            int t3 = 0;

          


            return false;
        }

    }
}
