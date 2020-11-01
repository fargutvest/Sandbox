using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using vkontakte;
using System.IO;
using Newtonsoft.Json.Linq;

namespace vkontakte
{
    class requestAPI_VK
    {
        public List<users> request(string query)
        {
            string responseFromServer = default(string);
            string uriString = string.Format("https://api.vk.com/method/{0}?oid={1}&access_token={2}", query, autorizeVK.USER_ID, autorizeVK.ACCESS_TOKEN);
            WebRequest _webRequest = WebRequest.Create(uriString);
            try
            {
                using (WebResponse _webResponce = _webRequest.GetResponse())
                {
                    Stream _stream = _webResponce.GetResponseStream();
                    StreamReader _streamReader = new StreamReader(_stream);
                    responseFromServer = _streamReader.ReadToEnd();
                    _streamReader.Close();
                }




                responseFromServer = HttpUtility.HtmlDecode(responseFromServer);
                JToken _jtoken = JToken.Parse(responseFromServer);
                List<users> UsersList = _jtoken["response"].Children().Skip(1).Select(c => c.ToObject<users>()).ToList();

                return UsersList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
