using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MedXTera.Classes
{
    public class Settings
    {
        static Settings()
        {
          Instance = new Settings();
        }
        private Settings()
        {
            LoadFromXMLDocument();
        }

        public static Settings Instance { get; private set; }


        #region Methods

        private string filename()
        {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\Settings.xml";
            // return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\result.xml";
        }


        // Загрузка значений полей из файла
        public void LoadFromXMLDocument()
        {
            BeginUpdate();
            try
            {

                if (File.Exists(filename()))
                {
                    // для чтения будет использоваться класс XmlReader
                    XmlReader xmlIn = XmlReader.Create(filename());
                    xmlIn.MoveToContent();

                    string lastElementName = "";
                    // цикл чтения файла
                    while (xmlIn.Read())
                    {
                        switch (xmlIn.NodeType)
                        {
                            // если найден новый тег
                            case XmlNodeType.Element:
                                lastElementName = xmlIn.Name;
                                break;
                            // если найден конец текущего тега
                            case XmlNodeType.EndElement:
                                if (xmlIn.Name == "MenuProperties")
                                    return;
                                break;

                            // получено значение тега
                            case XmlNodeType.Text:
                                PropertyInfo pi = this.GetType().GetProperty(lastElementName);
                                if (pi != null)
                                {
                                    // приводим значение тега к типу данных поля
                                    if (pi.PropertyType == typeof(string))
                                        pi.SetValue(this, xmlIn.Value, null);
                                    else if (pi.PropertyType == typeof(int))
                                        pi.SetValue(this, int.Parse(xmlIn.Value), null);
                                    else if (pi.PropertyType == typeof(bool))
                                        pi.SetValue(this, bool.Parse(xmlIn.Value), null);
                                    else if (pi.PropertyType == typeof(Color))
                                        pi.SetValue(this, Color.FromArgb(int.Parse(xmlIn.Value)), null);
                                }
                                break;

                        }
                    }
                    xmlIn.Close();
                }
            }
            finally
            {
                EndUpdate();
            }
        }



        ///Листинг 2. Сохранение значений всех полей класса в XML
        public bool SaveToXMLDocument()
        {
            try
            {
                XmlTextWriter xmlOut = new XmlTextWriter(filename(), System.Text.Encoding.UTF8);
                xmlOut.Formatting = Formatting.Indented;
                xmlOut.WriteStartDocument();

                // для записи будет использоваться класс XmlWriter
                //XmlWriter xmlOut = XmlWriter.Create(filename());

                // ИмяРаздела
                xmlOut.WriteStartElement(this.GetType().ToString());

                // получаем список всех полей текущего класса
                PropertyInfo[] pi = this.GetType().GetProperties();

                // цикл сохранения всех полей
                for (int i = 0; i < pi.Length; i++)
                {
                    //цвет сохраняем отдельно в виде числа
                    if (pi[i].PropertyType == typeof(Color))
                    {
                        Color c = (Color)(pi[i].GetValue(this, null));
                        xmlOut.WriteElementString(pi[i].Name, c.ToArgb().ToString());
                    }
                    else
                    {
                        object obj = pi[i].GetValue(this, null);
                        if (obj != null)
                            xmlOut.WriteElementString(pi[i].Name, obj.ToString());
                    }
                }

                xmlOut.WriteEndElement();
                xmlOut.Flush();
                xmlOut.Close();

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //                throw new ApplicationException(ex.Message);

                return false;

            }

            /*      XmlSerializer xmls = new XmlSerializer(this.GetType());
                   XmlTextWriter wr = new XmlTextWriter(filename(), System.Text.Encoding.UTF8);
                   wr.Formatting = Formatting.Indented;
                   xmls.Serialize(wr, this);
                   wr.Close();*/

        }

        #endregion




        public event EventHandler OnChange;

        private void DoChange()
        {
            if (isUpdate)
            {
                Modified = true;
            }
            else
            {
                Modified = false;

                if (OnChange != null)
                    OnChange.Invoke(this, null);
            }
        }

        private bool isUpdate = false;
        private bool Modified = false;

        public void BeginUpdate()
        {
            lock(this)
            {
                isUpdate = true;
            }
        }

        public void EndUpdate()
        {
            lock (this)
            {
                isUpdate = false;
                if (Modified)
                    DoChange();
            }
        }


        private const string DataBase_Settings = "Подключение к БД";
        private const string Global_Settings = "Глобальные";
        private const string Registration_Settings = "Регистрация";
        private const string Port1_Settings = "Подключение к генератору";
//        private const string Port2_Settings = "Подключение к фильтру и аппликатору";
        private const string Port3_Settings = "Подключение к клавиатуре";
        private const string Port4_Settings = "Подключение к таймеру";

        /*
                private string _Version = "1.0";
                [BrowsableAttribute(false)] // Указывает, следует ли отображать свойство или событие в окне "Свойства".
                [ReadOnlyAttribute(true)] // Указывает, что поле в окне "Свойства" только для чтения
                public string Version
                {
                    get { return _Version; }
                    set { _Version = value; }
                }
        */

        private Color _BackgroundColor = Color.Black;
        [CategoryAttribute(Global_Settings)]
        public Color BackgroundColor
        {
            get
            {
                lock (this)
                {
                    return _BackgroundColor;
                }
            }
            set {
                lock (this)
                {
                    if (_BackgroundColor != value)
                    {
                        _BackgroundColor = value;

                        DoChange();
                    }
                }
            }
        }

        private Color _TextColor = SystemColors.GrayText;
        [CategoryAttribute(Global_Settings)]
        public Color TextColor
        {
            get
            {
                lock (this)
                {
                    return _TextColor;
                }
            }
            set
            {
                lock (this)
                {
                    if (_TextColor != value)
                    {
                        _TextColor = value;

                        DoChange();
                    }
                }
            }
        }

        private Color _TextColor2 = SystemColors.ControlLight;
        [CategoryAttribute(Global_Settings)]
        public Color TextColor2
        {
            get
            {
                lock (this)
                {
                    return _TextColor2;
                }
            }
            set
            {
                lock (this)
                {
                    if (_TextColor2 != value)
                    {
                        _TextColor2 = value;

                        DoChange();
                    }
                }
            }
        }

        private bool _UseExtendedPersonName = false;
        [DisplayNameAttribute("Расширенное имя")]
        [DescriptionAttribute("Использовать расширенное имя человека")] // 1226, "Use extended person name"
        [CategoryAttribute(Registration_Settings)]
        [DefaultValueAttribute(false)]
        public bool UseExtendedPersonName
        {
            get
            {
                lock (this)
                {
                    return _UseExtendedPersonName;
                }
            }
            set {
                lock (this)
                {
                    if (_UseExtendedPersonName != value)
                    {
                        _UseExtendedPersonName = value;

                        DoChange();
                    }
                }
            }
        }


        private string _ODBC_Driver = "MySQL ODBC 5.2 Unicode Driver";
        [DisplayNameAttribute("ODBC драйвер")]
        [DescriptionAttribute("Имя драйвера для поключения к СУБД")]
        [CategoryAttribute(DataBase_Settings)]
        [DefaultValueAttribute("MySQL ODBC 5.2 Unicode Driver")]
        public string ODBC_Driver
        {
            get
            {
                lock (this)
                {
                    return _ODBC_Driver;
                }
            }
            set
            {
                lock (this)
                {
                    if (_ODBC_Driver != value)
                    {
                        _ODBC_Driver = value;

                        DoChange();
                    }
                }
            }
        }


        private string _server = "localhost";
        [DisplayNameAttribute("Сервер")]
        [DescriptionAttribute("Имя сервера для поключения к СУБД")]
        [CategoryAttribute(DataBase_Settings)]
        [DefaultValueAttribute("localhost")]
        public string Server
        {
            get
            {
                lock (this)
                {
                    return _server;
                }
            }
            set
            {
                lock (this)
                {
                    if (_server != value)
                    {
                        _server = value;

                        DoChange();
                    }
                }
            }
        }


        private int _DBPort = 3306;
        [DisplayNameAttribute("Порт")]
        [DescriptionAttribute("Номер порта для поключения к СУБД")]
        [CategoryAttribute(DataBase_Settings)]
        [DefaultValueAttribute(3306)]
        public int DBPort
        {
            get
            {
                lock (this)
                {
                    return _DBPort;
                }
            }
            set
            {
                lock (this)
                {
                    if (_DBPort != value && value >= 0 && value <= ushort.MaxValue)
                    {
                        _DBPort = value;

                        DoChange();
                    }
                }
            }
        }


        private string _DataBase = "MedXTera";
        [DisplayNameAttribute("База данных")]
        [DescriptionAttribute("Имя базы данных для поключения к СУБД")]
        [CategoryAttribute(DataBase_Settings)]
        [DefaultValueAttribute("MedXTera")]
        public string DataBase
        {
            get {
                lock (this)
                {
                    return _DataBase;
                }
            }
            set {
                lock (this)
                {
                    if (_DataBase != value)
                    {
                        _DataBase = value;

                        DoChange();
                    }
                }
            }
        }


        private string _Login = "root";
        [DisplayNameAttribute("Имя пользователя")]
        [DescriptionAttribute("Имя пользователя к базе данных")]
        [CategoryAttribute(DataBase_Settings)]
        [DefaultValueAttribute("root")]
        public string Login
        {
            get
            {
                lock (this)
                {
                    return _Login;
                }
            }
            set
            {
                lock (this)
                {
                    if (_Login != value)
                    {
                        _Login = value;

                        DoChange();
                    }
                }
            }
        }

        private string _Password = "";
        [DisplayNameAttribute("Пароль")]
        [DescriptionAttribute("Пароль к базе данных")]
        [CategoryAttribute(DataBase_Settings)]
        [DefaultValueAttribute("")]
        public string Password
        {
            get
            {
                lock (this)
                {
                    return _Password;
                }
            }
            set
            {
                lock (this)
                {
                    if (_Password != value)
                    {
                        _Password = value;

                        DoChange();
                    }
                }
            }
        }

/*        
        private string _Port2 = "COM2";
        [DisplayNameAttribute("COM-порт")]
        [DescriptionAttribute("Порт аппликатора и фильтра")]
        [CategoryAttribute(Port2_Settings)]
        [DefaultValueAttribute("COM2")]
        public string Port2
        {
            get
            {
                lock (this)
                {
                    return _Port2;
                }
            }
            set
            {
                lock (this)
                {
                    if (_Port2 != value)
                    {
                        _Port2 = value;

                        DoChange();
                    }
                }
            }
        }
*/
        // TODO: Скорость[Baudrate], Данные[Data], Паритет[Parity], Стоп бит[Stop bits], Упр. потоком [Flow control]

        private int _AddressFilter = 2;
        [DisplayNameAttribute("Адрес фильтра")]
        [DescriptionAttribute("Адрес фильтра")]
        [CategoryAttribute(Port4_Settings)]
        [DefaultValueAttribute(2)]
        public int AddressFilter
        {
            get
            {
                lock (this)
                {
                    return _AddressFilter;
                }
            }
            set
            {
                lock (this)
                {
                    if ((_AddressFilter != value) && (value >= 0) && (value <= 3) && (value != _AddressApplicator))
                    {
                        _AddressFilter = value;

                        DoChange();
                    }
                }
            }
        }

        private int _AddressApplicator = 3;
        [DisplayNameAttribute("Адрес аппликатора")]
        [DescriptionAttribute("Адрес аппликатора")]
        [CategoryAttribute(Port4_Settings)]
        [DefaultValueAttribute(3)]
        public int AddressApplicator
        {
            get
            {
                lock (this)
                {
                    return _AddressApplicator;
                }
            }
            set
            {
                lock (this)
                {
                    if ((_AddressApplicator != value) && (value >= 0) && (value <= 3) && (_AddressFilter != value))
                    {
                       _AddressApplicator = value;

                       DoChange();
                    }
                }
            }
        }
        

        private string _Port1 = "COM1";
        [DisplayNameAttribute("COM-порт")]
        [DescriptionAttribute("Порт генератора")]
        [CategoryAttribute(Port1_Settings)]
        [DefaultValueAttribute("COM1")]
        public string Port1
        {
            get
            {
                lock (this)
                {
                    return _Port1;
                }
            }
            set
            {
                lock (this)
                {
                    if (_Port1 != value)
                    {
                        _Port1 = value;

                        DoChange();
                    }
                }
            }
        }


        private string _Port3 = "COM3";
        [DisplayNameAttribute("COM-порт")]
        [DescriptionAttribute("Порт клавиатуры")]
        [CategoryAttribute(Port3_Settings)]
        [DefaultValueAttribute("COM3")]
        public string Port3
        {
            get
            {
                lock (this)
                {
                    return _Port3;
                }
            }
            set
            {
                lock (this)
                {
                    if (_Port3 != value)
                    {
                        _Port3 = value;

                        DoChange();
                    }
                }
            }
        }


        private string _Host = "192.168.0.3";
        [DisplayNameAttribute("IP-адрес")]
        [DescriptionAttribute("IP-адрес таймера")]
        [CategoryAttribute(Port4_Settings)]
        [DefaultValueAttribute("192.168.0.3")]
        public string Host
        {
            get
            {
                lock (this)
                {
                    return _Host;
                }
            }
            set
            {
                lock (this)
                {
                    if (_Host != value)
                    {
                        _Host = value;

                        DoChange();
                    }
                }
            }
        }


        private int _Port = 3000;
        [DisplayNameAttribute("Порт")]
        [DescriptionAttribute("Порт таймера")]
        [CategoryAttribute(Port4_Settings)]
        [DefaultValueAttribute(3000)]
        public int Port
        {
            get
            {
                lock (this)
                {
                    return _Port;
                }
            }
            set
            {
                lock (this)
                {
                    if (_Port != value)
                    {
                        _Port = value;

                        DoChange();
                    }
                }
            }
        }

    }
}



