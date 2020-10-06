using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace ViyarParser
{
    public class ExcelHelper
    {
        private IReport _report;

        public ExcelHelper(IReport report)
        {
            _report = report;
        }

        public string[][] ReadAllNotEmptyLines(string filePath, int[] columns)
        {
            var result = new List<string[]>();

            Application xlApp = null;
            dynamic xlRange = null;
            dynamic xlWorksheet = null;
            dynamic xlWorkbook = null;
            try
            {
                xlApp = new Application();
                xlWorkbook = xlApp.Workbooks.Open(filePath);
                xlWorksheet = xlWorkbook.Sheets[1];
                xlRange = xlWorksheet.UsedRange;

                int rowNumber = 1;
                string[] row = null;
                do
                {
                    row = new string[columns.Length];
                    _report.Write('*');
                    for (int i = 0; i < columns.Length; i++)
                    {
                        var value2 = xlRange.Cells[rowNumber, columns[i]].Value2;
                        row[i] = value2 != null ? value2.ToString() as string : "";
                    }
                    if (row.All(_ => string.IsNullOrEmpty(_) != false))
                    {
                        break;
                    }
                    result.Add(row);

                    rowNumber++;
                } while (row.All(_ => string.IsNullOrEmpty(_) == false));

            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);
                xlWorkbook.Close();
                //Marshal.ReleaseComObject(xlWorkbook);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }

            return result.ToArray();
        }
    }
}
