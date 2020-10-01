using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace ViyarParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var pages = new List<string>();

            var myClient = new WebClient();

            for (int i = 0; i < 7; i++)
            {
                var response = myClient.OpenRead($"https://viyar.by/catalog/dsp_1/page-{i + 1}/?view=60");
                var reader = new StreamReader(response);
                pages.Add(reader.ReadToEnd());
                response.Close();
            }

            var codeToImageMappings = new Dictionary<string, string>();

            foreach (var page in pages)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(page);

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-xs-12 col-sm-6 col-md-6 col-lg-4 product_prewiew-wrapper')]"))
                {
                    var innerHtml = node.InnerHtml;
                    var imgUrl = "https://viyar.by" + new Regex("src\\s*=\\s*\"(.+?)\"").Match(innerHtml).ToString().Replace("\"","").Replace("src=","");

                    var code = new Regex("<span>Код товара:(.+?)</span>").Match(innerHtml).ToString().Replace("<span>Код товара: ", "").Replace("</span>", "");
                    codeToImageMappings.Add(code, imgUrl);
                }
            }

            var remainsList = new List<string[]>();
            using (var reader = new StreamReader(@"stebeneva.csv", Encoding.GetEncoding(1251)))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',').ToList();
                    var code = values[1];
                    codeToImageMappings.TryGetValue(code, out var imgUrl);
                    remainsList.Add(new string[] { line, imgUrl });
                }
            }

            var resultHtml = "<!DOCTYPE html>" +
                             "<html>" +
                             "<body>" +
                             "<ul>";

            foreach (var item in remainsList)
            {
                resultHtml += "<li>" +
                    $"<img src='{item.Last()}' width='300'>" +
                    $"{item.First()}" +
                    "</li>";
            }

            resultHtml += "</ul>" +
                "</body>" +
                "</html>";

            var resultFilePath = "result.html";
            if (File.Exists(resultFilePath))
            {
                File.Delete(resultFilePath);
            }

            using (StreamWriter file = new StreamWriter(resultFilePath, true))
            {
                file.WriteLine(resultHtml);
            }
        }
    }
}


