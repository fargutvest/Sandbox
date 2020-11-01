using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using texternamespace;
namespace ConsoleApplication1
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string text = "Секреты построения невероятных размеров и силы Уже около тридцати лет Брэдли Д. Стейнер пишет в этом журнале о том, что старые методы набора массы и силы могут во многом быть полезны и современным бодибилдерам. Согласен с этим на 100%. Это было одной из причин написания мной книги Тренинг эпохи динозавров: забытые секреты силы и физического развития. Книга продавалась очень хорошо. Очевидно, многим людям интересно, что могут дать старые тренировочные методы современным силачам.";

            Console.WriteLine(text);
            Console.WriteLine();
            Console.WriteLine("Введите startindex");
            string startindex = Console.ReadLine();
            Console.WriteLine("Введите finishindex");
            string finishindex = Console.ReadLine();
            Console.WriteLine(texter.substrings(startindex, finishindex, text));
            Console.ReadKey();
            
            
        }
    }
}
