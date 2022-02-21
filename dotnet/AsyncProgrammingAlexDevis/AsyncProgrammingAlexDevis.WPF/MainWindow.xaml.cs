using System.Windows;

namespace AsyncProgrammingAlexDevis.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] _domains = { "google.com", "microsoft.com", "www.yahoo.com", "youtube.com", "www.facebook.com", "vk.com", "bing.com", "amazon.com", "yandex.by", "instagram.com" };

        private IChapter _chapter;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _chapter = new Chapter2(m_WrapPanel);

            foreach (var item in _domains)
            {
                _chapter.AddAFavicon(item);
            }
        }
    }
}
