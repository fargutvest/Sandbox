using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            string[] a = s.Split(new char[] { ' ' });
            for (int i = 0; i < a.Length; i++)
            {
                if (Convert.ToInt32(a[i]) % 2 == 0) Console.Write(a[i]+" ");
            }
            Console.ReadKey();
        }
        

        /// <summary>
/// "Какие грабли ожидают, в связи с тем что DataReceived будет
/// выполняться уже в другом потоке, и не в основном, и не в потоках опроса?" - очень не приятные.
/// Вам придется вводить дополнительный объект синхронизации, показывающий вам получен ответ или нет.
/// После чего вам придется решать проблему, а данный пакет от устройства действительно ответ на запрос
/// или же это мусор или кусок ответа на другой запрос. Windows не контроллер, по факту время получения
/// пакета по порту зависит от того как быстро вам драйвер порта передаст данные. Поэтому DataReceived - есть
/// куча проблем, а не решение проблемы.
/// </summary>
    }
}
