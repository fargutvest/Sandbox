using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RuTrackerParser
{
    internal class App
    {
        private HttpClient _client = new HttpClient();
        internal string Parse(string forumUrl, int pagesCount)
        {
            List<ParseableItem> parseableItems = GetHtmlPagesList(forumUrl, pagesCount);

            foreach (var item in parseableItems)
            {
                item.DoParsing();
            }

            List<string> reportList = parseableItems.OrderByDescending(_ => _.Seeders).Select(_ => _.ToString()).ToList();
            string reportString = string.Join(Environment.NewLine, reportList);
            return reportString;
        }

        private List<ParseableItem> GetHtmlPagesList(string url, int pagesCount)
        {
            List<ParseableItem> parseableItems = new List<ParseableItem>();

            ServicePointManager.DefaultConnectionLimit = Environment.ProcessorCount;
            Parallel.For(0, pagesCount, new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            }, i => 
            {
                string pageRoute = i == 0 ? "" : $"&start={i * 50}";
                string htmlPage = GetHtmlAsync(url, pageRoute).Result;
                List<string> htmlTableRows = htmlPage.Split(new string[] { @"</tr>" }, StringSplitOptions.None).Where(_ => _.Contains("Seeders")).ToList();
                parseableItems.AddRange(htmlTableRows.Select(_ => new ParseableItem()
                {
                    InnerHtml = _,
                    PageNumber = i + 1
                }));
            });
            return parseableItems.ToList();
        }

        private async Task<string> GetHtmlAsync(string url, string pageRoute) => await _client.GetStringAsync($"{url}{pageRoute}");
       
    }
}
