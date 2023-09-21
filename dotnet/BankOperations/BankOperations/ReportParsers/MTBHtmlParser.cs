using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using HtmlAgilityPack;

namespace BankOperations.ReportParsers
{
    public class MTBHtmlParser : IBankParser
    {
        private static string MTBReportsFolder => ConfigurationManager.AppSettings[nameof(MTBReportsFolder)];

        public ProgressReporter ProgressReporter { get; private set; }

        public MTBHtmlParser()
        {
            ProgressReporter = new ProgressReporter();
        }



        public List<Operation> Load()
        {
            var result = new List<Operation>();
            var files = Directory.GetFiles(MTBReportsFolder);

            for (int i = 0; i < files.Length; i++)
            {
                double percent = (i + 1) / (double)files.Length * 100;

                ProgressReporter.Report($"\rLoading {percent.ToString("F0")}%");

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

            return result;
        }

        private HtmlNode OpenNode(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode;
        }

        private Status GetStatus(object complete, object notCompleted, object operationIn)
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
