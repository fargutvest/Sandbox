using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Linq;

namespace BankOperations.ReportParsers
{
    public class PriorCsvParser : IBankParser
    {
        private static string PriorReportsFolder => ConfigurationManager.AppSettings[nameof(PriorReportsFolder)];

        public ProgressReporter ProgressReporter { get; private set; }

        public PriorCsvParser()
        {
            ProgressReporter = new ProgressReporter();
        }

        public List<Operation> Load()
        {
            var result = new List<Operation>();
            var files = Directory.GetFiles(PriorReportsFolder);

            for (int i = 0; i < files.Length; i++)
            {
                double percent = (i + 1) / (double)files.Length * 100;
                ProgressReporter.Report($"\rLoading {percent.ToString("F0")}%");

                var rows = File.ReadAllLines(files[i], Encoding.GetEncoding("windows-1251"));

                int rowIndex = 0;
                bool operationsBlock = false;
                bool inProgress = false;

                while (rowIndex < rows.Length)
                {
                    var currentRow = rows[rowIndex];

                    if (currentRow.Contains("Всего по контракту"))
                    {
                        operationsBlock = false;
                    }

                    if (currentRow.Contains("Операции по ........"))
                    {
                        rowIndex+=2;
                        operationsBlock = true;
                        continue;
                    }
                    if (currentRow.Contains("Заблокированные суммы по ........"))
                    {
                        rowIndex += 2;
                        operationsBlock = true;
                        inProgress = true;
                        continue;
                    }

                    if (operationsBlock)
                    {
                        var splitted = currentRow.Split(';');
                        var operation = new Operation();
                        operation.DateTime = DateTime.ParseExact(splitted[0], "dd.MM.yyyy HH:mm:ss", null);
                        operation.Title = splitted[1];
                        splitted[2] = splitted[2].Replace(',', '.').Replace(" ", "");
                        operation.Amount = double.Parse(splitted[2]);
                        operation.Currency = splitted[3];
                        operation.Place = splitted[8];
                        operation.Status = inProgress ? Status.InProgress : Status.Success; 

                        result.Add(operation);
                    }

                    rowIndex++;

                }

            }

            return result.OrderBy(_=>_.DateTime).ToList();
        }
    }
}
