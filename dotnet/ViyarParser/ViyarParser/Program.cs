using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ViyarParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var pages = new List<string>();

            var myClient = new WebClient();

            Console.WriteLine("Grabbing viyar.by ...");
            for (int i = 0; i < 7; i++)
            {
                var response = myClient.OpenRead($"https://viyar.by/catalog/dsp_1/page-{i + 1}/?view=60");
                var reader = new StreamReader(response);
                pages.Add(reader.ReadToEnd());
                response.Close();
                Console.Write('.');
            }

            var codeToImageMappings = new Dictionary<string, string>();
            var codeToPriceMappings = new Dictionary<string, string>();

            foreach (var page in pages)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(page);

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-xs-12 col-sm-6 col-md-6 col-lg-4 product_prewiew-wrapper')]"))
                {
                    var innerHtml = node.InnerHtml;
                    var imgUrl = "https://viyar.by" + new Regex("src\\s*=\\s*\"(.+?)\"").Match(innerHtml).ToString().Replace("\"","").Replace("src=","");

                    var code = new Regex("<span>Код товара:(.+?)</span>").Match(innerHtml).ToString().Replace("<span>Код товара: ", "").Replace("</span>", "");
                    var price = new Regex("<span class=\"price\">(.+?)</span>").Match(innerHtml).ToString().Replace("<span class=\"price\">", "").Replace("</span>", "");
                    var strike = new Regex("<strike class=\"price-odd\">(.+?)</strike>").Match(price).ToString();
                    price = string.IsNullOrEmpty(strike) ? price : price.Replace(strike, "");
                    codeToImageMappings.Add(code, imgUrl);
                    codeToPriceMappings.Add(code, price);
                    Console.Write('.');
                }
            }

            var fileName = "stebeneva1.xls";
            new WebClient().DownloadFile("https://viyar.by/upload/ex_files/stebeneva.xls", fileName);

            var remainsList = new List<EntryModel>();
            Application xlApp = null;
            dynamic xlRange = null;
            dynamic xlWorksheet = null;
            dynamic xlWorkbook = null;
            try
            {
                xlApp = new Application();
                xlWorkbook = xlApp.Workbooks.Open($@"{Environment.CurrentDirectory}\{fileName}");
                xlWorksheet = xlWorkbook.Sheets[1];
                xlRange = xlWorksheet.UsedRange;

                int row = 2;
                while (xlRange.Cells[row, 2].Value2 != null)
                {
                    var entry = new EntryModel()
                    {
                        Code = xlRange.Cells[row, 2].Value2.ToString().Replace(" ", String.Empty),
                        Nomenklature = xlRange.Cells[row, 3].Value2.ToString(),
                        Characteristic = Size.Parse(xlRange.Cells[row, 4].Value2.ToString().Replace("х", ",")),
                        Thikness = xlRange.Cells[row, 5].Value2.ToString(),
                        Count = double.Parse(xlRange.Cells[row, 6].Value2.ToString())
                    };

                    codeToImageMappings.TryGetValue(entry.Code, out var imgUrl);
                    codeToPriceMappings.TryGetValue(entry.Code, out var price);
                    entry.ImageUrl = imgUrl;
                    entry.Price = price;
                    
                    entry.Cost = entry.Price!= null ? (entry.Count / (2070 * 2800 / (double)1000000) * double.Parse(entry.Price.Replace(" руб/лист", ""))).ToString("0.##") + "руб" : null;
                    remainsList.Add(entry);
                    row++;
                    Console.Write('.');
                } 
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

            var resultDoc = new HtmlDocument();

            var grouped = remainsList.GroupBy(_ => _.Nomenklature).ToDictionary(_=> _.Key, _=>_.OrderBy(x=>x.Count).ToList());

            foreach (var item in grouped)
            {

                resultDoc.DocumentNode.AppendChild(HtmlNode.CreateNode($@"<li>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <img src = '{item.Value.First().ImageUrl}' width='300'>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div><b>{item.Value.First().Code}</b> - {item.Key}</div>
                                                                                        {string.Join("", item.Value.Select(_ => $"<div>{_.Characteristic.ToString().Replace(",", "х")} ({_.Count}) - <b>{_.Cost}</b> ({_.Price})</div>").ToList())}
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                          </li>"));
                Console.Write('.');
            }

            var resultFilePath = "result.html";
            if (File.Exists(resultFilePath))
            {
                File.Delete(resultFilePath);
            }

            using (StreamWriter file = new StreamWriter(resultFilePath, true))
            {
                file.WriteLine(resultDoc.DocumentNode.OuterHtml);
            }

           
            
            Process.Start(resultFilePath);
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}


