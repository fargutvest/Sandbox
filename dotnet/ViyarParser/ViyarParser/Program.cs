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
using System.Configuration;

namespace ViyarParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var pages = new List<string>();

            var myClient = new WebClient();

            Console.WriteLine("Grabbing viyar.by ...");
            for (int i = 0; i < int.Parse(ConfigurationManager.AppSettings["pagesCount"]); i++)
            {
                var response = myClient.OpenRead($"https://viyar.by/catalog/dsp_1/page-{i + 1}/?view={ConfigurationManager.AppSettings["pageSize"]}");
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
                    var imgUrl = "https://viyar.by" + new Regex("src\\s*=\\s*\"(.+?)\"").Match(innerHtml).ToString().Replace("\"", "").Replace("src=", "");

                    var code = new Regex("<span>Код товара:(.+?)</span>").Match(innerHtml).ToString().Replace("<span>Код товара: ", "").Replace("</span>", "");
                    var price = new Regex("<span class=\"price\">(.+?)</span>").Match(innerHtml).ToString().Replace("<span class=\"price\">", "").Replace("</span>", "");
                    var strike = new Regex("<strike class=\"price-odd\">(.+?)</strike>").Match(price).ToString();
                    price = string.IsNullOrEmpty(strike) ? price : price.Replace(strike, "");
                    codeToImageMappings.Add(code, imgUrl);
                    codeToPriceMappings.Add(code, price);
                    Console.Write('.');
                }
            }

            var fileName = ConfigurationManager.AppSettings["stebenevaFileName"];
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

                int row = int.Parse(ConfigurationManager.AppSettings["firstRow"]);
                while (xlRange.Cells[row, ConfigurationManager.AppSettings["codeColumn"]].Value2 != null)
                {
                    var entry = new EntryModel();
                    entry.Code = xlRange.Cells[row, ConfigurationManager.AppSettings["codeColumn"]].Value2.ToString().Replace(" ", String.Empty);
                    entry.Nomenklature = xlRange.Cells[row, ConfigurationManager.AppSettings["nomenklatureColumn"]].Value2.ToString();
                    entry.Characteristic = Size.Parse(xlRange.Cells[row, ConfigurationManager.AppSettings["characteristicColumn"]].Value2.ToString().Replace("х", ","));
                    entry.Thikness = xlRange.Cells[row, ConfigurationManager.AppSettings["thiknessColumn"]].Value2.ToString();
                    entry.Count = double.Parse(xlRange.Cells[row, ConfigurationManager.AppSettings["countColumn"]].Value2.ToString());
                    
                    codeToImageMappings.TryGetValue(entry.Code, out var imgUrl);
                    codeToPriceMappings.TryGetValue(entry.Code, out var price);
                    entry.ImageUrl = imgUrl;
                    entry.Price = price;

                    entry.Cost = entry.Price != null ? (entry.Count / (2070 * 2800 / (double)1000000) * double.Parse(entry.Price.Replace(" руб/лист", ""))).ToString("0.##") + "руб" : null;
                    remainsList.Add(entry);
                    row++;
                    Console.Write('.');
                }
            }
            catch (Exception ex)
            {

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


