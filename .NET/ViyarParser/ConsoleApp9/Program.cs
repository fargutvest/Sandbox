using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleApp9
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
                var doc  = new HtmlDocument();
                doc.LoadHtml(page);

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-xs-12 col-sm-6 col-md-6 col-lg-4 product_prewiew-wrapper')]"))
                {
                    var innerHtml = node.InnerHtml;
                    var a = innerHtml.Substring(innerHtml.IndexOf("/upload/resize_cache/photos/300_300_1/"));
                    var imgUrl = "https://viyar.by/" + a.Substring(0, a.IndexOf(".jpg") + 4);


                    a = innerHtml.Substring(innerHtml.IndexOf("<span>Код товара: ") + 18);
                    var code  = a.Substring(0, a.IndexOf("</span>"));
                    codeToImageMappings.Add(code, imgUrl);
                }
            }

            var remainsList = new List<string[]>();
            using (var reader = new StreamReader(@"stebeneva.csv", Encoding.GetEncoding(1251)))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var code = values[1];
                    codeToImageMappings.TryGetValue(code, out var imgUrl);
                    var withUrl = values.ToList();
                    withUrl.Add(imgUrl);

                    remainsList.Add(withUrl.ToArray());
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
                    $"{string.Join(",", item)}" +
                    "</li>";
            }

            resultHtml += "</ul>" +
                "</body>" +
                "</html>";


            using (StreamWriter file = new StreamWriter(@"result.html", true))
            {
                file.WriteLine(resultHtml);
            }
        }

    }

}
