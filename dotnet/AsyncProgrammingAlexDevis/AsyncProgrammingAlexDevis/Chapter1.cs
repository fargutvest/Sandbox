using System;
using System.Net;

namespace AsyncProgrammingAlexDevis
{
    public class Chapter1
    {
        public void DumpWebPage(string uri)
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(uri);
            Console.WriteLine(page);
        }

        public async void DumpWebPageAsync(string uri)
        {
            WebClient webClient = new WebClient();
            string page = await webClient.DownloadStringTaskAsync(uri);
            Console.WriteLine(page);
        }
    }
}
