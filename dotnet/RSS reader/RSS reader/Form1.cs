using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.Xsl;

namespace RSS_reader
    
{
    public partial class Form1 : Form 
    {
        string flFName = "feed_list.log"; // список лент, на которые мы подписались 
        RSS rss; // текущая RSS-лента 

        public Form1()
        {
            InitializeComponent();
            if (File.Exists(flFName))
                listBox1.Items.AddRange(File.ReadAllLines(flFName));  
        }

        void LoadFeed(string URL)
        {
            try
            {
                rss = new RSS(URL);
                webBrowser1.Navigate(Application.StartupPath + "\\" + rss.TransformToHTML());
            }
            catch (System.Net.WebException exc)
            {
                MessageBox.Show(exc.Message, " Нет подключения к интернету ");
            }
        }

        void UpdateFeedListFile()
        {
            string bak_file = "feed_list.bak";
            try
            {
                if (File.Exists(bak_file)) File.Delete(bak_file);
                File.Move(flFName, bak_file);
                if (File.Exists(flFName)) File.Delete(flFName);
                foreach (string item in listBox1.Items)
                    File.AppendAllText(flFName, item + "\n");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        // выбрать ленту    
        void LbRSSClick(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        // показать ленту    
        void LbRSSDoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
            LoadFeed(textBox1.Text);
        }

        //добавить ленту в подписку 
        void bNewClick(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                string newURL = textBox1.Text.Trim();
                listBox1.Items.Add(newURL);
                File.AppendAllText(flFName, newURL + "\n");
            }
        }
        //изменить адрес ленты в подписке 
        void BEditClick(object sender, EventArgs e)
        {
            listBox1.Items[listBox1.SelectedIndex] = textBox1.Text.Trim();
            UpdateFeedListFile();
        }

        //удалить ленту из подписки 
        void BDeleteClick(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;
            listBox1.Items.RemoveAt(idx);
            if (idx >= listBox1.Items.Count) idx--;
            if (idx >= 0) listBox1.SetSelected(idx, true);
            UpdateFeedListFile();
        }

        // сохранить текущую ленту    
        void SaveAsHTMLToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                File.Move(Application.StartupPath + "\\feed.html", saveFileDialog1.FileName);
        }


    
    }
   
    public class RSS
    {
        private string _URL;
        private string XML_FileName;
        private string HTML_FileName;
        private string XSL_FileName;

        

        public RSS(string URL) : this(URL, "feed.xsl") // перегрузка
        {

        }

        public RSS(string URL, string XSLT_File) //конструктор класса
        {
            if (!Directory.Exists("files")) Directory.CreateDirectory("files");
            XML_FileName = @"files\" + EscapeUrl(URL) + ".xml";
            HTML_FileName = @"files\" + EscapeUrl(URL) + ".htm";
            long dt = 0;
            if (File.Exists(XML_FileName))
                dt = DateTime.Now.Ticks - File.GetCreationTime(XML_FileName).Ticks;
            XSL_FileName = XSLT_File;
            _URL = URL;
            if (!(File.Exists(XML_FileName) && dt < 3600e8))
                /* если лента уже была обновлена в течении предыдущего часа  
           * то загрузим её из локального файла */
                try
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    client.DownloadFile(_URL, XML_FileName);
                    string sXML = File.ReadAllText(XML_FileName, Encoding.GetEncoding(1251));
                    sXML = Regex.Replace(sXML, @"<\?xml-stylesheet.*\?>", "");
                    sXML = Regex.Replace(sXML, @"<\w+\sxmlns:xsi.*\.xsd.?\s*>", "");
                    sXML = Regex.Replace(sXML, @"<!DOCTYPE.*>", "");
                    File.WriteAllText(XML_FileName, sXML, Encoding.GetEncoding(1251));
                }
                catch (XmlException e)
                {
                    MessageBox.Show(e.Message, " Ошибка загрузки или обработки XML");
                }
        }


        private string EscapeUrl(string s)
        {
            return
          s.Replace('/', '_').Replace(".xml", "").Replace("http:__", "").Replace("www.", "");
        }

        public string TransformToHTML()
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(XSL_FileName);
            xslt.Transform(XML_FileName, HTML_FileName);
            return HTML_FileName;
        }   
    }


  
}
