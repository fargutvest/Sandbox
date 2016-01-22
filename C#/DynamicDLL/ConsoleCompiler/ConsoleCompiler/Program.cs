using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;

namespace ConsoleCompiler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Source code для компиляции
            string source =
            @"
using System.Collections.Generic;
using System.Linq;

namespace Foo
{
    public class Bar
    {
        public static void SayHello()
        {
            System.Console.WriteLine(""Hello World"");
            System.Console.WriteLine( string.Join("","", Enumerable.Range(0,10).Select(n=>n.ToString()).ToArray() ) );
        }
    }
}
            ";

            // Настройки компиляции
            Dictionary<string, string> providerOptions = new Dictionary<string, string>
                {
                    {"CompilerVersion", "v3.5"}
                };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            const string outputAssembly = "D:\\Foo.dll";

            CompilerParameters compilerParams = new CompilerParameters
                {OutputAssembly = outputAssembly, GenerateExecutable = false};
            compilerParams.ReferencedAssemblies.Add("System.Core.Dll");

            // Компиляция
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, source);

            // Выводим информацию об ошибках
            Console.WriteLine("Number of Errors: {0}", results.Errors.Count);
            foreach (CompilerError err in results.Errors)
            {
                Console.WriteLine("ERROR {0}", err.ErrorText);
            }

            Console.WriteLine("Try Assembly:");
            Assembly assembly = Assembly.LoadFile(outputAssembly);
            Type type = assembly.GetType("Foo.Bar");
            MethodInfo method = type.GetMethod("SayHello");
            method.Invoke(null, null);

            Console.ReadKey();
        }
    }
}