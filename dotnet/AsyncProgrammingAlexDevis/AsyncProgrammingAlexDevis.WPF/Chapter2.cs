using System.Net;
using System.Windows.Controls;
using Image = System.Windows.Controls.Image;

namespace AsyncProgrammingAlexDevis.WPF
{
    public class Chapter2 : Base,  IChapter
    {
        public Chapter2(WrapPanel wrapPanel) : base(wrapPanel) { }
   
        public void AddAFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData($"http://{domain}/favicon.ico");
            Image imageControl = MakeImageControl(bytes);
            m_WrapPanel.Children.Add(imageControl);
        }
    }
}
