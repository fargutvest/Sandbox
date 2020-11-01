using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using System.Web;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Music_plauer
{
    public partial class Form1 : Form
    {
        IWMPPlaylist Playlist;
        IWMPMedia Media;

        string ACCESS_TOKEN;
        string USER_ID;
        string MY_ID = "6811515";
        XmlDocument xd = new XmlDocument();
        List<Audio> AudioList = new List<Audio>();
        int inc = 0;
        public Form1()
        {
            InitializeComponent();
            webBrowser1.Navigated += webBrowser1_Navigated;
            string uriString = 
                "http://oauth.vk.com/authorize?client_id=4273691&scope=friends,audio&redirect_uri=https://oauth.vk.com/blank.html&display=popup&response_type=token";
            webBrowser1.Url = new System.Uri(uriString, System.UriKind.Absolute);   
        }

        

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
          
            if (e.Url.ToString().IndexOf("http://oauth.vk.com/blank.html") != -1)
            {
                var urlParams = HttpUtility.ParseQueryString(e.Url.Fragment.Substring(1));
                ACCESS_TOKEN = urlParams.Get("access_token");
                USER_ID = urlParams.Get("user_id");
                /*string uriString =
                       string.Format("https://api.vk.com/method/{0}.xml?oid={1}&access_token={2}",
                      "audio.get", "6811515", ACCESS_TOKEN);
                       webBrowser1.Navigate(uriString);*/
             request();
            }
         /*   if (e.Url.AbsolutePath.ToString().IndexOf("/method/audio.get.xml") != -1)
            {
                
                XmlDocument xd = new XmlDocument();

                xd.LoadXml(webBrowser1.DocumentText.ToString().Replace("&nbsp;"," "));
                //XmlNodeList urls = xd.SelectNodes()
                //var document = xd.Load(webBrowser1.DocumentStream);
            }*/


            

        }

        void request()
        {
            string uriString =
                       string.Format("https://api.vk.com/method/{0}?oid={1}&access_token={2}",
                      "audio.get", MY_ID, ACCESS_TOKEN);
            WebRequest _webRequest = WebRequest.Create(uriString);
            WebResponse _webResponce = _webRequest.GetResponse();
            Stream _stream = _webResponce.GetResponseStream();
            StreamReader _streamReader = new StreamReader(_stream);
            string responseFromServer = _streamReader.ReadToEnd();
            _streamReader.Close();
            _webResponce.Close();
            responseFromServer = HttpUtility.HtmlDecode(responseFromServer);
            JToken _jtoken = JToken.Parse(responseFromServer);
            AudioList = _jtoken["response"].Children().Skip(1).Select(c => c.ToObject<Audio>()).ToList();
            this.Invoke((MethodInvoker)delegate
            {
                Playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("vkPLayList");
                for (int i = 0; i < AudioList.Count; i++)
                {
                    
                    Media = axWindowsMediaPlayer1.newMedia(AudioList[i].url);
                    Playlist.appendItem(Media);
                    listView1.Items.Add(AudioList[i].artist + " " + AudioList[i].title);
                }
                axWindowsMediaPlayer1.currentPlaylist = Playlist;
                axWindowsMediaPlayer1.Ctlcontrols.stop();

            });
        }
       
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.Ctlcontrols.currentItem = axWindowsMediaPlayer1.currentPlaylist.get_Item(listView1.SelectedItems[0].Index);
            }
        }


    }

    public class Audio
    {
        public int aid { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string url {get; set;}
        public string lirics_id {get; set;}
        public int genre {get;set;}
    }

    
}
