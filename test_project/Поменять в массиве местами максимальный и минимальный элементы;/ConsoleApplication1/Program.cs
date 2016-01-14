using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
   
    class Program
    {
        static void Main(string[] args)
        {
            int mem_max_index=0;
            int mem_min_index=0;
            int compare_max = 0;
            int compare_min = 2147483647;
            int[] massiv = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // массив
            string dispaly = "";
          
            foreach (int s in massiv)
            {
                dispaly += s + " ";
            }
            Console.WriteLine(dispaly);

            for (int t = 0; t < massiv.Length; t++)
            {
                if (massiv[t] > compare_max)
                {
                    compare_max = massiv[t];
                    mem_max_index = t;
                }

                if (massiv[t] < compare_min)
                {
                    compare_min = massiv[t];
                    mem_min_index = t;
                }
            }
            int buff;
            buff = massiv[mem_max_index];
            massiv[mem_max_index] = massiv[mem_min_index];
            massiv[mem_min_index] = buff;
            dispaly = "";
            foreach (int s in massiv)
            {
                dispaly += s+" ";
            }
            Console.Write(dispaly);
            Console.ReadLine();
        }
    }
}
