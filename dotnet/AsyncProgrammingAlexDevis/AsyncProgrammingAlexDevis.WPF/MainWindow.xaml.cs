using System.Windows;

namespace AsyncProgrammingAlexDevis.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] _domains = { "google.com", "microsoft.com", "www.yahoo.com", "youtube.com", "www.facebook.com", "vk.com", "bing.com", "amazon.com", "yandex.by", "instagram.com" };
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var chapter = new Chapter7(m_WrapPanel);

            chapter.OnClickAndWhenAny(_domains);
        }
    }
}
