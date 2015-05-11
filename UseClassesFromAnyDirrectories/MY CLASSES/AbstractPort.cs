using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Adani
{
    public abstract class AbstractPort : IDisposable, IPort
    {

        public virtual event EventHandler DataReceived;

        public abstract void PortChange(string port);

        public abstract void open();

        public abstract void close();

        public abstract bool IsOpen();

        public abstract void write(byte[] b);


        

        public abstract byte[] read();

        public abstract void Dispose();
    
    }
}
