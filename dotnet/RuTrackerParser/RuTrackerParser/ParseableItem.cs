using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RuTrackerParser
{
    internal class ParseableItem
    {
        /// <summary>
        /// 1 - based.
        /// </summary>
        public int PageNumber { get; set; }
        public string InnerHtml { get; set; }

        public string Title { get; set; }

        public int Seeders { get; set; }

        public string Url { get; set; }

        public string Size { get; set; }

        public override string ToString()
        {
            return $"[{Seeders}] [{Size}] Url:{Url} {Title} ";
        }

        public void DoParsing()
        {
            Seeders = GetSeedersCount(InnerHtml);
            Title = GetTitle(InnerHtml);
            Url = GetUrl(InnerHtml);
            Size = GetSize(InnerHtml);
        }

        private List<string> SplitHtmlToLines(string html) => html.Split('\n').ToList();


        private int GetSeedersCount(string htmlTableRow)
        {
            List<int> result = GetOrderedSeedersList(htmlTableRow);
            return result.Any() ? result[0] : -1;
        }

        private List<int> GetOrderedSeedersList(string html)
        {
            List<string> seedersRows = SplitHtmlToLines(html).Where(_ => _.Contains("Seeders")).ToList();

            Regex regex = new Regex(@"title=""Seeders""><b>\d*</b>");
            List<int> seedersValues = seedersRows.Select(_ => int.Parse(regex.Match(_).Value.Replace(@"title=""Seeders""><b>", "").Replace("</b>", ""))).OrderByDescending(x => x).ToList();
            return seedersValues;
        }

        private string GetTitle(string htmlTableRow)
        {
            Regex regex = new Regex(@"class=""torTopic bold tt-text"">.*</a>");
            string line = SplitHtmlToLines(htmlTableRow).Where(_ => regex.IsMatch(_)).FirstOrDefault();
            string title = regex.Match(line).Value.Replace("class=\"torTopic bold tt-text\">", "").Replace("</a>", "");
            return title;
        }

        private string GetUrl(string htmlTableRow)
        {
            Regex regex = new Regex("href=\"viewtopic.php\\?t=\\d*\"");
            string line = SplitHtmlToLines(htmlTableRow).Where(_ => regex.IsMatch(_)).FirstOrDefault();

            if (string.IsNullOrEmpty(line))
                return "";

            string href = regex.Match(line).Value.Replace("href=", "").Replace("\"", "");
            return "https://rutracker.net/forum/" + href;
        }

        private string GetSize(string htmlTableRow)
        {
            Regex regex = new Regex("class=\"small f-dl dl-stub\">.*&nbsp;GB</a>");
            string line = SplitHtmlToLines(htmlTableRow).Where(_ => regex.IsMatch(_)).FirstOrDefault();

            if (string.IsNullOrEmpty(line))
                return "";

            string size = regex.Match(line).Value.Replace("class=\"small f-dl dl-stub\">", "").Replace("&nbsp;GB</a>", "");
            return size + "GB";
        }
    }
}
