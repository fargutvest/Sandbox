using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Windows.Controls;


namespace vkontakte
{
    public static class autorizeVK
    {
        public static string ACCESS_TOKEN;
        public static string USER_ID;
        public static void  autorize(WebBrowser _webBrowser)
        {
            string clientID = "4273691";
            string url = string.Format("http://oauth.vk.com/authorize?client_id={0}&scope=friends,audio&redirect_uri=https://oauth.vk.com/blank.html&display=popup&response_type=token", clientID);
            _webBrowser.Navigated += _webBrowser_Navigated;
            _webBrowser.Navigate(url);
        }

        static void _webBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
            if (e.Uri.ToString().IndexOf("http://oauth.vk.com/blank.html") != -1)
            {
                var urlParams = HttpUtility.ParseQueryString(e.Uri.Fragment.Substring(1));
                ACCESS_TOKEN = urlParams.Get("access_token");
                USER_ID = urlParams.Get("user_id");
            }
            
        }
    }
}
