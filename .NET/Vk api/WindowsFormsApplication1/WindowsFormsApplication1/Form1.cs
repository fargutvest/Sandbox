using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            test();
        }

        void test()
        {
            int appId = 4273691; // указываем id приложения
            string email = "genadys@mail.ru"; // email для авторизации
            string password = "golovorot123579"; // пароль
            Settings settings = Settings.All; // уровень доступа к данным

            var api = new VkApi();
            api.Authorize(appId, email, password, settings); // авторизуемся

            var group = api.Utils.ResolveScreenName("habr"); // получаем id сущности с коротким именем habr

            // получаем id пользователей из группы, макс. кол-во записей = 1000
            int totalCount; // общее кол-во участников
            var userIds = api.Groups.GetMembers(group.Id.Value, out totalCount);
            /*foreach (long id in userIds)
            {
                api.Messages.Send(id, false, "привет, друг!"); // посылаем сообщение пользователю
            }*/
        }
    }
}

