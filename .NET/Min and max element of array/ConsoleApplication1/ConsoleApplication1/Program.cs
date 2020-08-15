using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static int[] a;
        static void Main(string[] args)
        {
            Console.WriteLine("введите размерность:");
            try
            {
                a = new int[Convert.ToInt32(Console.ReadLine())];
                for (int t = 0; t < a.Length ; t++)
                {
                    Console.WriteLine("введите " + (t+1).ToString()+  " элемент массива:");
                    a[t] = Convert.ToInt32(Console.ReadLine());
                }

                int min = a[0];
                for (int t = 0; t < a.Length ; t++)
                {
                    if (min > a[t]) min = a[t];
                }

                int max = 0;
                for (int t = 0; t < a.Length ; t++)
                {
                    if (max < a[t]) max = a[t];
                }

                Console.WriteLine();
                Console.WriteLine("минимальный - " +min+ ", максимальный - "+max);
                Console.ReadKey();
            }

            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            
        }
    }
}
