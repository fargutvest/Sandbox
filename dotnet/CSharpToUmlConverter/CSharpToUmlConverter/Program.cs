using System;
using System.IO;

namespace CSharpToUmlConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("Wrong arguments");
                return;
            }

            var root = args[0];
            var outputPath = args[1];

            if (!Directory.Exists(root))
            {
                Console.WriteLine("Specified root directory is not exist.");
                return;
            }


            if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
            {
                Console.WriteLine("Specified output directory is not exist.");
                return;
            }

            new Converter().Start(root, outputPath);
        }
    }
}
