using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections;
using Newtonsoft.Json;
using MTBReportParser;
using System.Globalization;
using System.Threading.Tasks;

namespace MTBReportParser
{
    class Program
    {
        private static List<string> _userInputHistory = new List<string>();
        private static int _historyCurrent = 0;

        private static string CacheFileName => ConfigurationManager.AppSettings[nameof(CacheFileName)];
        private static string TemplatesFileName => ConfigurationManager.AppSettings[nameof(TemplatesFileName)];
        private static string ReportsFolder => ConfigurationManager.AppSettings[nameof(ReportsFolder)];


        static void Main(string[] args)
        {
            LoadCache();
            
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            var operations = new List<Operation>();
            operations.AddRange(Load());

            foreach (var item in operations)
            {
                if (item.Amount > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                if (item.Status!= Status.Success)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.WriteLine(item);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            var console = new ListeningUserInput(_userInputHistory);
            var help = "help";
            var exit = "exit";
            var templates = "templates";
            while (true)
            {
                var input = console.Listen(ref _historyCurrent);

                if (input == help)
                {
                    Console.WriteLine($"{Environment.NewLine}{new string('*', Console.WindowWidth - 1)}");
                    ;
                    Console.WriteLine(
                        $"Use '_' to access collection." +
                        $"{Environment.NewLine}Use 'templates YYYY.mm.dd YYYY.mm.dd' to access predefined templates. Example: 'templates 2021.03.01 2021.04.01'" +
                        $"{Environment.NewLine}Last item as json:" +
                        $"{Environment.NewLine}{JsonConvert.SerializeObject(operations.Last(), Formatting.Indented)}" +
                        $"{Environment.NewLine}Types:" +
                        $"{Environment.NewLine}{string.Join(Environment.NewLine, typeof(Operation).GetProperties(System.Reflection.BindingFlags.Public| System.Reflection.BindingFlags.Instance).Select(_ => $"{_.PropertyType} {_.Name}"))}" +
                        $"{Environment.NewLine}{Environment.NewLine}To get templates input 'templates YYYY.mm.dd YYYY.mm.dd' Example: 'templates 2021.03.01 2021.04.01'");
                    Console.WriteLine($"{new string('*', Console.WindowWidth-1)}{Environment.NewLine}");
                }
                else if (input == exit)
                {
                    Console.WriteLine("Bye");
                    Task.Delay(300).Wait();
                    break;
                }
                else
                {
                    try
                    {
                        if (input.Contains(templates))
                        {
                            var lines = File.ReadAllLines(TemplatesFileName).ToList();
                            for (int i = 0; i < lines.Count(); i++)
                            {
                                Console.WriteLine($"{i + 1}: {lines[i]}");
                            }
                            Console.WriteLine("Choose template:");
                            var choose = int.Parse(Console.ReadKey().KeyChar.ToString()) - 1;
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
                            Console.WriteLine();
                        }

                        var scriptOptions = ScriptOptions.Default
                            .AddImports("System.Linq")
                            .AddImports("System")
                            .AddImports("MTBReportParser")
                            .AddReferences(typeof(Enumerable).Assembly)
                            .AddReferences(typeof(Status).Assembly);

                        var result = CSharpScript.EvaluateAsync(
                                   input,
                                   scriptOptions, globals: new Global() { _ = operations }).Result;


                        if (typeof(IEnumerable).IsAssignableFrom(result.GetType()))
                        {
                            var objectsCollection = (result as IEnumerable).Cast<object>();
                            var stringLines = objectsCollection.Select(_ => _.ToString()).ToList();
                            var toConsole = string.Join(Environment.NewLine, stringLines);
                            Console.WriteLine(toConsole);
                        }
                        else
                        {
                            Console.WriteLine(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine($"Input '{help}' to get more info.");
                    }
                    SaveCache();
                }
            }
        }

      

        private static HtmlNode OpenNode(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode;
        }

        private static Status GetStatus(object complete, object notCompleted, object operationIn)
        {
            if (complete != null && notCompleted == null && operationIn == null)
            {
                return Status.Success;
            }
            else if (complete == null && notCompleted != null && operationIn == null)
            {
                return Status.Decline;
            }
            else if (complete == null && notCompleted == null && operationIn != null)
            {
                return Status.InProgress;
            }
            else
            {
                return Status.Unknown;
            }
        }

        private static List<Operation> Load()
        {
            var result = new List<Operation>();
            var files = Directory.GetFiles(ReportsFolder);

            for (int i = 0; i < files.Length; i++)
            {
                double percent = (i + 1) / (double)files.Length * 100;
                Console.Write($"\rLoading {percent.ToString("F0")}%");

                var raw = File.ReadAllText(files[i]);
                var days = OpenNode(raw).SelectNodes("//div[contains(@class, 'operations-by-day-row payment-history')]").ToList();

                var listNodes = new List<Operation>();

                foreach (var node in days.Select(_ => OpenNode(_.InnerHtml)))
                {
                    var date = node.SelectSingleNode("//div[contains(@class, 'day-header')]").InnerText;
                    date = date.Contains("Сегодня") ? date.Replace("Сегодня", "") : date;
                    var dayOperations = node.SelectNodes("//div[contains(@class, 'operation-row-wrap')]");

                    foreach (var operation in dayOperations.Select(_ => OpenNode(_.InnerHtml)))
                    {
                        var complete = operation.SelectSingleNode("//div[contains(@class, 'operation-status complete')]");
                        var notCompleted = operation.SelectSingleNode("//div[contains(@class, 'operation-status operation-not-completed')]");
                        var operationIn = operation.SelectSingleNode("//div[contains(@class, 'operation-status operation-in')]");
                        var status = GetStatus(complete, notCompleted, operationIn);
                        var title = operation.SelectNodes("//div[contains(@class, 'operation-title')]")[1].InnerText.Replace("\r\n", "");
                        var place = operation.SelectSingleNode("//div[contains(@class, 'operation-place')]").InnerText.Replace("\r\n", "");
                        var time = operation.SelectSingleNode("//div[contains(@class, 'operation-time')]").InnerText;
                        var currencyNode = operation.SelectSingleNode("//div[contains(@class, 'operation-sum-currency-main')]");
                        var amount = currencyNode != null ? double.Parse($"{currencyNode.ChildNodes[0].InnerText}{currencyNode.ChildNodes[1].InnerText}".Replace(" ", "").Replace("\r\n", "").Replace(".", ",")) : 0;
                        var currency = currencyNode?.ChildNodes[2]?.InnerText.Replace(" ", "");

                        var dateTime = DateTime.Parse($"{date.Replace(",", "").Replace("года", "")} {time}", new CultureInfo("ru-RU"));
                        listNodes.Add(new Operation() { DateTime = dateTime, Title = title, Place = place, Amount = amount, Currency = currency, Status = status });
                    }
                }

                listNodes.Reverse();
                result.AddRange(listNodes);
            }
            Console.WriteLine();

            return result;
        }

        private static void SaveCache()
        {
            if (File.Exists(CacheFileName))
            {
                File.Delete(CacheFileName);
            }
            File.WriteAllLines(CacheFileName, _userInputHistory.Distinct());
        }

        private static void LoadCache()
        {
            try
            {
                _userInputHistory = File.ReadAllLines(CacheFileName).ToList();
                _historyCurrent = _userInputHistory.Count();
            }
            catch 
            {
            }
        }
    }

}
