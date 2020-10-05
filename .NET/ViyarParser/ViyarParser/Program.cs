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

namespace ViyarParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var codeToImageMappings = new Dictionary<string, string>();
            var codeToPriceMappings = new Dictionary<string, string>();

            var myClient = new WebClient();
            HtmlNodeCollection nodes = null;
            var pageNum = 1;
            var isLastPageAchieved = false;
            do
            {
                Console.Write('.');
                var response = myClient.OpenRead($"https://viyar.by/catalog/dsp_1/page-{pageNum}/?view=60");
                var reader = new StreamReader(response);
                var page = reader.ReadToEnd();
                response.Close();

                var doc = new HtmlDocument();
                doc.LoadHtml(page);
                nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-xs-12 col-sm-6 col-md-6 col-lg-4 product_prewiew-wrapper')]");
                foreach (HtmlNode node in nodes)
                {
                    Console.Write('o');
                    var innerHtml = node.InnerHtml;
                    var imgUrl = "https://viyar.by" + new Regex("src\\s*=\\s*\"(.+?)\"").Match(innerHtml).ToString().Replace("\"", "").Replace("src=", "");

                    var code = new Regex("<span>Код товара:(.+?)</span>").Match(innerHtml).ToString().Replace("<span>Код товара: ", "").Replace("</span>", "");
                    var price = new Regex("<span class=\"price\">(.+?)</span>").Match(innerHtml).ToString().Replace("<span class=\"price\">", "").Replace("</span>", "");
                    var strike = new Regex("<strike class=\"price-odd\">(.+?)</strike>").Match(price).ToString();
                    price = string.IsNullOrEmpty(strike) ? price : price.Replace(strike, "");
                    if (codeToImageMappings.ContainsKey(code))
                    {
                        isLastPageAchieved = true;
                        break;
                    }
                    codeToImageMappings.Add(code, imgUrl);
                    codeToPriceMappings.Add(code, price);
                }
                pageNum++;
            } while (isLastPageAchieved == false);

            var fileName = "stebeneva3.xls";
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
                    Console.Write('*');
                    var entry = new EntryModel()
                    {
                        Code = xlRange.Cells[row, 2].Value2.ToString(),
                        Nomenklature = xlRange.Cells[row, 3].Value2.ToString(),
                        Characteristic = Size.Parse(xlRange.Cells[row, 4].Value2.ToString().Replace("х", ",")),
                        Thikness = xlRange.Cells[row, 5].Value2.ToString(),
                        Count = double.Parse(xlRange.Cells[row, 6].Value2.ToString())
                    };

                    codeToImageMappings.TryGetValue(entry.Code, out var imgUrl);
                    codeToPriceMappings.TryGetValue(entry.Code, out var price);
                    entry.ImageUrl = imgUrl;
                    entry.Price = price;

                    entry.Cost = entry.Price != null ? (entry.Count / (2070 * 2800 / (double)1000000) * double.Parse(entry.Price.Replace(" руб/лист", ""))).ToString("0.##") + "руб" : null;
                    remainsList.Add(entry);
                    row++;
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

            var grouped = remainsList.GroupBy(_ => _.Nomenklature).ToDictionary(_ => _.Key, _ => _.OrderBy(x => x.Count).ToList());

            foreach (var item in grouped)
            {
                Console.Write('"');
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
        }
    }
}


