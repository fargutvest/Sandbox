using System;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BankOperations.ReportParsers;
using BankOperations.Helpers;

namespace BankOperations
{
    public class Application
    {
        private string TemplatesFileName => ConfigurationManager.AppSettings[nameof(TemplatesFileName)];
        private Banks TargetBank = (Banks)Enum.Parse(typeof(Banks), ConfigurationManager.AppSettings[nameof(TargetBank)]);

        private const string HELP = "help";
        private const string EXIT = "exit";
        private const string TEMPLATES = "templates";

        private ConsoleHelper _consoleHelper = new ConsoleHelper();


        public void Start()
        {
            var cache = new Cache();
            cache.Load();

            IBankParser bankParser = null;
            switch (TargetBank)
            {
                case Banks.Prior:
                    bankParser = new PriorCsvParser();
                    break;
                case Banks.MTBank:
                    bankParser = new MTBHtmlParser();
                    break;
            }
            
            bankParser.ProgressReporter.OnProgress += BankParser_OnProgress;

            _consoleHelper.UseUnicode();

            var operations = bankParser.Load();

            _consoleHelper.WriteLine();

            foreach (var item in operations)
            {
                if (item.Status != Status.Success)
                {
                    _consoleHelper.WriteLine(item, ConsoleColor.DarkGray);
                }
                else if (item.Amount > 0)
                {
                    _consoleHelper.WriteLine(item, ConsoleColor.DarkGreen);
                }
                else
                {
                    _consoleHelper.WriteLine(item);
                }
            }

            var userInput = new ListeningUserInput(cache.UserInputHistory, _consoleHelper);
            
            while (true)
            {
                var input = userInput.Listen();
                if (input == HELP)
                {
                    _consoleHelper.WriteSeparatorBeginLine();

                    _consoleHelper.WriteLine(
                        $"Use '_' to access collection." +
                        $"{Environment.NewLine}Use 'templates YYYY.mm.dd YYYY.mm.dd' to access predefined templates. Example: 'templates 2021.03.01 2021.04.01'" +
                        $"{Environment.NewLine}Last item as json:" +
                        $"{Environment.NewLine}{JsonConvert.SerializeObject(operations.Last(), Formatting.Indented)}" +
                        $"{Environment.NewLine}Types:" +
                        $"{Environment.NewLine}{string.Join(Environment.NewLine, typeof(Operation).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Select(_ => $"{_.PropertyType} {_.Name}"))}" +
                        $"{Environment.NewLine}{Environment.NewLine}To get templates input 'templates YYYY.mm.dd YYYY.mm.dd' Example: 'templates 2021.03.01 2021.04.01'");
                    _consoleHelper.WriteSeparatorEndLine();
                }
                else if (input == EXIT)
                {
                   _consoleHelper.WriteLine("Bye");
                    Task.Delay(300).Wait();
                    break;
                }
                else
                {
                    try
                    {
                        if (input.Contains(TEMPLATES))
                        {
                            var lines = File.ReadAllLines(TemplatesFileName).ToList();
                            for (int i = 0; i < lines.Count(); i++)
                            {
                               _consoleHelper.WriteLine($"{i + 1}: {lines[i]}");
                            }
                           _consoleHelper.WriteLine("Choose template:");
                            var choose = int.Parse(_consoleHelper.Read()) - 1;
                            var choosedTemplate = lines[choose];
                            if (choosedTemplate.Contains("%FROM%"))
                            {
                                choosedTemplate = choosedTemplate.Replace("%FROM%", $"new DateTime({input.Split(' ')[1].Replace('.', ',')})");
                            }
                            if (choosedTemplate.Contains("%TO%"))
                            {
                                choosedTemplate = choosedTemplate.Replace("%TO%", $"new DateTime({input.Split(' ')[2].Replace('.', ',')})");
                            }
                            input = choosedTemplate;
                           _consoleHelper.WriteLine();
                        }
 
                        var result = DoScript(input, operations);

                        if (typeof(IEnumerable).IsAssignableFrom(result.GetType()))
                        {
                            var objectsCollection = (result as IEnumerable).Cast<object>();
                            var stringLines = objectsCollection.Select(_ => _.ToString()).ToList();
                            var toConsole = string.Join(Environment.NewLine, stringLines);
                           _consoleHelper.WriteLine(toConsole);
                        }
                        else
                        {
                           _consoleHelper.WriteLine(result);
                        }
                    }
                    catch (Exception ex)
                    {
                       _consoleHelper.WriteLine(ex.Message);
                       _consoleHelper.WriteLine($"Input '{HELP}' to get more info.");
                    }

                    cache.Save();
                }
            }
        }

        private void BankParser_OnProgress(string text)
        {
            _consoleHelper.Write(text);
        }

        private object DoScript(string input, List<Operation> operations)
        {
            var scriptOptions = ScriptOptions.Default
                           .AddImports("System.Linq")
                           .AddImports("System")
                           .AddImports(nameof(BankOperations))
                           .AddReferences(typeof(Enumerable).Assembly)
                           .AddReferences(typeof(Status).Assembly);

            var result = CSharpScript.EvaluateAsync(
                       input,
                       scriptOptions, globals: new Global() { _ = operations }).Result;

            return result;
        }
    }

}
