using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            /*MyUserSettings myusersettings = new MyUserSettings();
            myusersettings.Name = "MyName";
            myusersettings.Save();*/

            Properties.Settings.Default.IPAdress = "192.168.1.133";
            
            
            Console.WriteLine(Properties.Settings.Default.IPAdress);
            Console.WriteLine(Properties.Settings.Default.LocalPort.ToString());
            Console.WriteLine(Properties.Settings.Default.RemotePort.ToString());

            

            Console.ReadKey();

            
        }

        
    }
}
