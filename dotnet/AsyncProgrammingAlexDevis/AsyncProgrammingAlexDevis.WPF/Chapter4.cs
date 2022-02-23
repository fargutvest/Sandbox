using System;
using System.Net;
using System.Windows.Controls;

namespace AsyncProgrammingAlexDevis.WPF
{
    public class Chapter4 : Base
    {
        public Chapter4(WrapPanel wrapPanel) : base(wrapPanel) { }

        public async void AddAFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            var bytes = await webClient.DownloadDataTaskAsync(new Uri("http://" + domain + "/favicon.ico"));
            Image imageControl = MakeImageControl(bytes);
            m_WrapPanel.Children.Add(imageControl);
        }
        
    }
}
