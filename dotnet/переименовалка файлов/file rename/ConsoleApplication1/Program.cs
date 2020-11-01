using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {


        static void Main(string[] args)
        {
            string patch = @"f:\torename";
            foreach (var file in Directory.EnumerateFiles(patch))//путь
            {
                Console.WriteLine(file);
                
            }
            Console.ReadLine();
            
            
            
            
            
            
            
            
            /* string name="";
            string patch1 = @"F:\1\abc.txt";
            
            Console.Write("Hi \r\n");
            name = Console.ReadLine();
            string patch2 = @"F:\1\" + name + ".txt";
            File.Move(patch1,patch2);

            Console.ReadLine();*/
        }
    }
}
