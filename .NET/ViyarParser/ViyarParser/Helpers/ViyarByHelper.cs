using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ViyarParser
{
    public class ViyarByHelper
    {
        private IReport _report;
        public ViyarByHelper(IReport report)
        {
            _report = report;
        }

        public Dictionary<int, string[]> GrabCatalogDsp()
        {
            var result = new Dictionary<int, string[]>();

            var myClient = new WebClient();
            HtmlNodeCollection nodes = null;
            var pageNum = 1;
            var isLastPageAchieved = false;
            do
            {
                Console.Write('.');
                string page = "";
                try
                {
                    var response = myClient.OpenRead($"https://viyar.by/catalog/dsp_1/page-{pageNum}/?view=60");
                    var reader = new StreamReader(response);
                    page = reader.ReadToEnd();
                    response.Close();
                }
                catch (Exception)
                {
                    isLastPageAchieved = true;
                }
                
                var doc = new HtmlDocument();
                doc.LoadHtml(page);
                nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-xs-12 col-sm-6 col-md-6 col-lg-4 product_prewiew-wrapper')]");
                foreach (HtmlNode node in nodes)
                {
                    _report.Write('o');
                    var innerHtml = node.InnerHtml;
                    var imgUrl = "https://viyar.by" + new Regex("src\\s*=\\s*\"(.+?)\"").Match(innerHtml).ToString().Replace("\"", "").Replace("src=", "");

                    var code = int.Parse(new Regex("<span>Код товара:(.+?)</span>").Match(innerHtml).ToString().Replace("<span>Код товара: ", "").Replace("</span>", ""));
                    var price = new Regex("<span class=\"price\">(.+?)</span>").Match(innerHtml).ToString().Replace("<span class=\"price\">", "").Replace("</span>", "");
                    var strike = new Regex("<strike class=\"price-odd\">(.+?)</strike>").Match(price).ToString();
                    price = string.IsNullOrEmpty(strike) ? price : price.Replace(strike, "");
                    if (result.ContainsKey(code))
                    {
                        isLastPageAchieved = true;
                        break;
                    }
                    result.Add(code, new string[] { imgUrl, price });
                }
                pageNum++;
            } while (isLastPageAchieved == false);

            return result;
        }

        public void DownloadRemainsList(string fileName)
        {
            new WebClient().DownloadFile("https://viyar.by/upload/ex_files/stebeneva.xls", fileName);
        }
    }
}
