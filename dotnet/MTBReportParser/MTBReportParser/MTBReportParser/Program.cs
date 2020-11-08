using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections;

namespace MTBReportParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var operations = new List<Operation>();

            foreach (var item in Directory.GetFiles(args[0]))
            {
                var raw = File.ReadAllText(item);
                var days = OpenNode(raw).SelectNodes("//div[contains(@class, 'operations-by-day-row payment-history')]").ToList();

                var listNodes = new List<Operation>();

                foreach (var node in days.Select(_ => OpenNode(_.InnerHtml)))
                {
                    var date = node.SelectSingleNode("//div[contains(@class, 'day-header')]").InnerText;
                    var dayOperations = node.SelectNodes("//div[contains(@class, 'operation-row-wrap')]");

                    foreach (var operation in dayOperations.Select(_ => OpenNode(_.InnerHtml)))
                    {
                        var complete = operation.SelectSingleNode("//div[contains(@class, 'operation-status complete')]");
                        var notCompleted = operation.SelectSingleNode("//div[contains(@class, 'operation-status operation-not-completed')]");
                        var operationIn = operation.SelectSingleNode("//div[contains(@class, 'operation-status operation-in')]");
                        var status = GetStatus(complete, notCompleted, operationIn);
                        var title = operation.SelectNodes("//div[contains(@class, 'operation-title')]")[1].InnerText;
                        var place = operation.SelectSingleNode("//div[contains(@class, 'operation-place')]").InnerText;
                        var time = operation.SelectSingleNode("//div[contains(@class, 'operation-time')]").InnerText;
                        var currencyNode = operation.SelectSingleNode("//div[contains(@class, 'operation-sum-currency-main')]");
                        var amount = currencyNode != null ? double.Parse($"{currencyNode.ChildNodes[0].InnerText}{currencyNode.ChildNodes[1].InnerText}".Replace(" ", "").Replace(",", ".")) : 0;
                        var currency = currencyNode?.ChildNodes[2]?.InnerText;

                        var dateTime = DateTime.Parse($"{date.Replace(",", "").Replace("года", "")} {time}", new CultureInfo("ru-RU"));
                        listNodes.Add(new Operation() { DateTime = dateTime, Title = title, Place = place,  Amount = amount, Currency = currency , Status = status });
                    }
                }

                listNodes.Reverse();
                operations.AddRange(listNodes);
            }


            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.WriteLine(string.Join(Environment.NewLine, operations.Where(_ => _.Status != Status.Decline).Select(_ => _.ToString())));

            while (true)
            {
                var input = Console.ReadLine();

                try
                {
                    var scriptOptions = ScriptOptions.Default.AddImports("System.Linq").AddImports("System").AddReferences(typeof(Enumerable).Assembly);
                    var result = CSharpScript.EvaluateAsync(
                               input,
                               scriptOptions, globals: new Global() { _ = operations }).Result;


                    if (typeof(IEnumerable).IsAssignableFrom(result.GetType()))
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, (result as IEnumerable<object>).Select(_ => _.ToString())));
                    }
                    else
                    {
                        Console.WriteLine(result);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
    }

}
