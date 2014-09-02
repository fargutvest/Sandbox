using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            generationXMLfile();
            useXmlTextWriter();
            readFileXmlDocument();
            readXMLfile();

        }



        void readFileXmlDocument()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(@"d:\SECURITYSYSTEMS\Gilson\SampleChanger\SampleChanger\SampleChanger\bin\Debug\Racks.xml");
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("RackEntry");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
               XmlNodeList childNodeList = xmlNode.ChildNodes;
               foreach (XmlNode childNode in childNodeList)
               {
               }
            }
            
        }

        void useXmlTextWriter()
        {
            XmlTextWriter xmlOut = new XmlTextWriter("fileXmlTextWriter.xml", System.Text.Encoding.UTF8);
            xmlOut.Formatting = Formatting.Indented;
            xmlOut.WriteStartDocument();
            xmlOut.WriteStartElement("My_root");

            xmlOut.WriteStartElement("elem1");
            xmlOut.WriteStartAttribute("atr1");
            xmlOut.WriteEndAttribute();
            xmlOut.WriteAttributeString("Atr2", "valAtr");
            xmlOut.WriteElementString("param1", "valueN");
            xmlOut.WriteEndElement();

            xmlOut.WriteEndElement();
            xmlOut.Flush();
            xmlOut.Close();
        }


        void generationXMLfile()
        {
            XmlWriter textWriter = new XmlTextWriter("file.xml", Encoding.UTF8); //Создаём сам XML-файл
            textWriter.WriteStartDocument(); //Создаём в файле заголовок XML-документа
            textWriter.WriteStartElement("Settings"); //Создём голову
            textWriter.WriteEndElement(); //Закрываем её
            textWriter.Close(); //И закрываем наш XmlTextWriter

            //Для занесения данных мы будем использовать класс XmlDocument
            XmlDocument document = new XmlDocument();
            document.Load("file.xml"); //Загружаем наш файл

            //Создаём XML-записи

            XmlNode typePortCMD = document.CreateElement("TypePortCMD"); // даём имя
            document.DocumentElement.AppendChild(typePortCMD); // указываем родителя
            typePortCMD.InnerText = "TCP";


            XmlNode typePortIMG = document.CreateElement("TypePortIMG");
            document.DocumentElement.AppendChild(typePortIMG); 
            typePortIMG.InnerText = "UDP";


            XmlNode EthernetSettings = document.CreateElement("EthernetSettings");
            document.DocumentElement.AppendChild(EthernetSettings);

            XmlNode IP = document.CreateElement("IP"); 
            IP.InnerText = "192.168.2.1";
            EthernetSettings.AppendChild(IP);

            XmlNode CMDport = document.CreateElement("CMDport");
            CMDport.InnerText = "3000";
            EthernetSettings.AppendChild(CMDport);

            XmlNode IMGport = document.CreateElement("IMGport");
            IMGport.InnerText = "4001";
            EthernetSettings.AppendChild(IMGport);


            XmlNode COMportSettings = document.CreateElement("COMportSettings");
            document.DocumentElement.AppendChild(COMportSettings);

            XmlNode COMport = document.CreateElement("COMport");
            COMport.InnerText = "COM2";
            COMportSettings.AppendChild(COMport);

            XmlNode BaudRate = document.CreateElement("BaudRate");
            BaudRate.InnerText = "9600";
            COMportSettings.AppendChild(BaudRate);


            XmlNode TimeOut = document.CreateElement("TimeOut");
            document.DocumentElement.AppendChild(TimeOut);

            XmlNode cmd = document.CreateElement("cmd");
            cmd.InnerText = "10000";
            TimeOut.AppendChild(cmd);

            XmlNode img = document.CreateElement("img");
            img.InnerText = "10000";
            TimeOut.AppendChild(img);


            XmlNode PixelDepth = document.CreateElement("PixelDepth");
            document.DocumentElement.AppendChild(PixelDepth);
            PixelDepth.InnerText = "16bit";


            XmlNode ImageSettings = document.CreateElement("ImageSettings");
            document.DocumentElement.AppendChild(ImageSettings);

            XmlNode Height = document.CreateElement("Height");
            Height.InnerText = "512";
            ImageSettings.AppendChild(Height);

            XmlNode Width = document.CreateElement("Width");
            Width.InnerText = "512";
            ImageSettings.AppendChild(Width);

            XmlNode GrabMode = document.CreateElement("GrabMode");
            GrabMode.InnerText = "Frame";
            ImageSettings.AppendChild(GrabMode);

            XmlNode UsePixelNumber = document.CreateElement("UsePixelNumber");
            UsePixelNumber.InnerText = "true";
            ImageSettings.AppendChild(UsePixelNumber);

            XmlNode FrameGrabberOffset = document.CreateElement("FrameGrabberOffset");
            FrameGrabberOffset.InnerText = "20";
            ImageSettings.AppendChild(FrameGrabberOffset);


            XmlNode DetectorNumber = document.CreateElement("DetectorNumber");
            document.DocumentElement.AppendChild(DetectorNumber);
            DetectorNumber.InnerText = "1";

            
            document.Save("file.xml");



        }

        void readXMLfile()
        {
            // Объявляем и забиваем файл в документ   
            XmlDocument xd = new XmlDocument();
            FileStream fs = new FileStream("file.xml", FileMode.Open);
            xd.Load(fs);


            XmlNodeList TypePortCMD = xd.GetElementsByTagName("TypePortCMD");
            richTextBox1.Text += string.Format("TypePortCMD: {0}\r\n", TypePortCMD[0].InnerText);

            XmlNodeList TypePortIMG = xd.GetElementsByTagName("TypePortIMG");
            richTextBox1.Text += string.Format("TypePortIMG: {0}\r\n", TypePortIMG[0].InnerText);

            XmlNodeList CMDport = xd.GetElementsByTagName("CMDport");
            richTextBox1.Text += string.Format("CMDport: {0}\r\n", CMDport[0].InnerText);

            

            /*
            XmlNodeList list = xd.GetElementsByTagName("element"); // Создаем и заполняем лист по тегу "user"   
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement one = (XmlElement)xd.GetElementsByTagName("subElementOne")[i];         // Забиваем id в переменную   
                XmlElement two = (XmlElement)xd.GetElementsByTagName("subElementTwo")[i];      // Забиваем login в переменную   
                XmlElement three = (XmlElement)xd.GetElementsByTagName("subElementThree")[i];   // Забиваем password в переменную   
                richTextBox1.Text += string.Format("{0} \r\n", one.InnerText);
                richTextBox1.Text += string.Format("{0} \r\n", two.InnerText);
                richTextBox1.Text += string.Format("{0} \r\n", three.InnerText);
            }
            */
            // Закрываем поток   
            fs.Close();

        }
    }
}
