using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace MvcApplication2.Models
{
    public class TextModel
    {
        public string text { get; set; }

        public TextModel(string id)
        {
            getText(id);
        }

        public void getText(string page)
        {
            FileStream aFile = new FileStream(page, FileMode.Open);
            StreamReader sr = new StreamReader(aFile, Encoding.GetEncoding(1251));
            text = sr.ReadToEnd();
            sr.Close();
        }
    }
}