using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace habrJob
{


    public enum Education
    {
        None,
        Higher,
        IncompleteHigher,
        SecondaryVocational,
        Secondary,
        Pupil
    }

    public enum Employment
    {
        Full,
        Partial,
        Freelance
    }

    public class HabraJob
    {
        public HabraJob()
        {
        }

        private string _price = string.Empty;

        public string Url { get; set; }

        public string Title { get; set; }

        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                if (_price != "з/п договорная")
                {
                    PriceMoney = int.Parse(value.Substring(3).Replace(" ", ""));
                }
            }
        }
        public int PriceMoney { get; set; }

        public string Company { get; set; }

        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }

        public Education Education { get; set; }

        public Employment Employment { get; set; }

        public static Education ParseEducation(string education)
        {
            switch (education)
            {
                case "Высшее":
                    return Education.Higher;
                case "Неполное высшее":
                    return Education.IncompleteHigher;
                case "Среднее специальное":
                    return Education.SecondaryVocational;
                case "Среднее":
                    return Education.Secondary;
                case "Учащийся":
                    return Education.Pupil;
                case "Не имеет значения":
                    return Education.None;
                default:
                    throw new Exception();
            }
        }

        public static Employment ParseEmployment(string employment)
        {
            switch (employment)
            {
                case "полная":
                    return Employment.Full;
                case "частичная":
                    return Employment.Partial;
                case "фриланс":
                    return Employment.Freelance;
                default:
                    throw new Exception();
            }
        }
    }

    public class WM
    {
        public string name;
        public int id;

        public WM(string _name, int _id)
        {
            name = _name;
            id = _id;
        }
    }

    class Program
    {
        public static WebClient wClient;
        public static WebRequest request;
        public static WebResponse response;

        public static List<HabraJob> jobList;

        public static Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

        static int GetPagesCount(HtmlDocument html)
        {
            var liNodes = html.GetElementbyId("nav-pages").ChildNodes.Where(x => x.Name == "li");

            HtmlAttribute href = liNodes.Last().FirstChild.Attributes["href"];

            int pagesCount = (int)Char.GetNumericValue(href.Value[href.Value.Length - 2]);

            return pagesCount;
        }


        static void GetJobLinks(HtmlDocument html)
        {
            List<WM> list1 = new List<WM>();

            foreach (HtmlNode table in html.DocumentNode.SelectNodes("//table"))
            {
                Console.WriteLine("Found: " + table.Id);
                foreach (HtmlNode row in table.SelectNodes("//tr"))
                {
                    var cells = row.SelectNodes("td");
                    if (cells.Count == 5)
                    {

            //            list1.Add(new WM(cells[0].InnerText, Convert.ToInt32(cells[1].InnerText, 16)));
                        list1.Add(new WM(cells[4].InnerText, Convert.ToInt32(cells[3].InnerText, 16)));

              //          Console.WriteLine("const int " + cells[0].InnerText + " = " + cells[1].InnerText + ";");
                        Console.WriteLine("const int " + cells[4].InnerText + " = " + cells[3].InnerText + ";");
                    }
                }
            }

           
        }

        static void GetFullInfo(HabraJob job)
        {
            HtmlDocument html = new HtmlDocument();
            // html.LoadHtml(wClient.DownloadString(job.Url));
            html.LoadHtml(GetHtmlString(job.Url));

            // так делать нельзя :-(
            var table = html.GetElementbyId("main-content").ChildNodes[1].ChildNodes[9].ChildNodes[1].ChildNodes[2].ChildNodes[1].ChildNodes[3].ChildNodes.Where(x => x.Name == "tr").ToArray();

            foreach (var tr in table)
            {
                string category = tr.ChildNodes.FindFirst("th").InnerText;

                switch (category)
                {
                    case "Компания":
                        job.Company = tr.ChildNodes.FindFirst("td").FirstChild.InnerText;
                        break;
                    case "Образование:":
                        job.Education = HabraJob.ParseEducation(tr.ChildNodes.FindFirst("td").InnerText);
                        break;
                    case "Занятость:":
                        job.Employment = HabraJob.ParseEmployment(tr.ChildNodes.FindFirst("td").InnerText);
                        break;
                    default:
                        continue;
                }
            }
        }

        public static string GetHtmlString(string url)
        {
            request = WebRequest.Create(url);
            request.Proxy = null;
            response = request.GetResponse();
            using (StreamReader sReader = new StreamReader(response.GetResponseStream(), encode))
            {
                return sReader.ReadToEnd();
            }
        }

        public static void SerializeToXml(List<HabraJob> jobList)
        {
            using (TextWriter output = new StreamWriter("report.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<HabraJob>));
                serializer.Serialize(output, jobList);
            }
        }

        static void Main(string[] args)
        {
            jobList = new List<HabraJob>();
            wClient = new WebClient();

            wClient.Proxy = null;
            wClient.Encoding = encode;

            HtmlDocument html = new HtmlDocument();

            string url = "https://www.autoitscript.com/autoit3/docs/appendix/WinMsgCodes.htm";
            string path = "Windows Message Codes.htm";
            html.LoadHtml(wClient.DownloadString(path));
            GetJobLinks(html);

            int pagesCount = GetPagesCount(html);

            for (int i = 2; i <= pagesCount; i++)
            {
                html.LoadHtml(wClient.DownloadString(string.Format("http://habrahabr.ru/job/page{0}", i)));
                GetJobLinks(html);
            }

            foreach (var job in jobList)
            {
                GetFullInfo(job);
            }

            SerializeToXml(jobList);
        }
    }
}

