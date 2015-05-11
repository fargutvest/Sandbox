using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adani
{
    interface IPort
    {
        void PortChange(string port);

        void open();


        void close();

        bool IsOpen();


        void write(byte[] b);


        byte[] read();

        void Dispose();
    }
}
