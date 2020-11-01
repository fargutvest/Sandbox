using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vkontakte;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageWebBrowser());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<users> listU = default(List<users>);
            requestAPI_VK _requestApi = new requestAPI_VK();
            try
            {
                listU = _requestApi.request(textBox1.Text);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }
            listView1.Items.Clear();
            foreach(users u in listU)
            {
                listView1.Items.Add(string.Format("first name: {0}, last name: {1}, uid: {2}",u.first_name,u.last_name,u.uid));
            }
                
        }
    }
}
