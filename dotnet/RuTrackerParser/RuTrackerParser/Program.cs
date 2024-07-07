using System;
using System.Diagnostics;
using System.IO;

namespace RuTrackerParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = new App();

            while (true)
            {
                try
                {
                    Console.WriteLine("Введите URL:");
                    string url = Console.ReadLine();
                    Console.WriteLine("Введите количество страниц:");
                    int pagesCount = int.Parse(Console.ReadLine());
                    string report = app.Parse(url, pagesCount);
                    File.WriteAllText("temp.txt", report);
                    Process.Start("temp.txt");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
