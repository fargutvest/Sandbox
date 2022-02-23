using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AsyncProgrammingAlexDevis.WPF
{
    public class Chapter7 : Base
    {
        public Chapter7(WrapPanel wrapPanel) : base(wrapPanel) { }

        public async void OnClick(string[] s_Domains)
        {
            IEnumerable<Task<Image>> tasks = s_Domains.Select(GetFavicon);
            tasks = tasks.ToList();
            Task<Image[]> allTask = Task.WhenAll(tasks);

            Image[] images = await allTask;
            foreach (Image eachImage in images)
            {
                AddFavicon(eachImage);
            }
        }

        public async void OnClickAndWhenAny(string[] s_Domains)
        {
            IEnumerable<Task<Image>> tasks = s_Domains.Select(GetFavicon);
            tasks = tasks.ToList();
            Task<Task<Image>> anyTask = Task.WhenAny(tasks);
            Task<Image> winner = await anyTask;
            Image image = await winner;

            AddFavicon(image);

            foreach (Task<Image> eachTask in tasks)
            {
                if (eachTask != winner)
                {
                    var eachImage = await eachTask;
                    AddFavicon(eachImage);
                }
            }
        }

        private async Task<Image> GetFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            var bytes = await webClient.DownloadDataTaskAsync(new Uri("http://" + domain + "/favicon.ico"));
            Image imageControl = MakeImageControl(bytes);
            return imageControl;
        }

        private void AddFavicon(Image imageControl)
        {
            m_WrapPanel.Children.Add(imageControl);
        }
    }
}
