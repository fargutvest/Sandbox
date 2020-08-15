using System;
using System.Linq;

namespace SandboxIEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            var cats = new Cats();
            var catIEnumerable = cats.Cast<Cat>();
            var catList = catIEnumerable.ToList();

            
            foreach (var item in catIEnumerable)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine(new string('-', 50));

            catList.ForEach(_ => Console.WriteLine(_.Name));

            Console.ReadKey();
        }
    }
}
