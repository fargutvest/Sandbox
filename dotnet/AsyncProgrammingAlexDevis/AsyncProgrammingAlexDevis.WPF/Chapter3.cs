using System;
using System.Net;
using System.Windows.Controls;

namespace AsyncProgrammingAlexDevis.WPF
{
    public class Chapter3 : Base, IChapter
    {
        public Chapter3(WrapPanel wrapPanel) : base(wrapPanel) { }

        public void AddAFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadDataCompleted += OnWebClientOnDownloadDataCompleted;
            webClient.DownloadDataAsync(new Uri("http://" + domain + "/favicon.ico"));
        }

        private void OnWebClientOnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs args)
        {
            Image imageControl = MakeImageControl(args.Result);
            m_WrapPanel.Children.Add(imageControl);
        }
    }
}
