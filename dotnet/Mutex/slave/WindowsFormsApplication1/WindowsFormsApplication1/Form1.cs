using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;
namespace Slave
{
    /// 
    /// Summary description for Form1.
    /// 
    public class Form1 : System.Windows.Forms.Form
    {

        // Переменная - мьютекс
        private Mutex mutex;
        private FileInfo file;
        // С помощью данной переменной мы будем читать информацию из файла
        private StreamReader stream;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox data;

        /// 
        /// Required designer variable.
        /// 
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();


        }

        /// 
        /// Clean up any resources being used.
        /// 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// 
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// 
        private void InitializeComponent()
        {
            this.data = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // data
            // 
            this.data.AcceptsReturn = true;
            this.data.Location = new System.Drawing.Point(8, 16);
            this.data.Multiline = true;
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(224, 248);
            this.data.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(256, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 56);
            this.button1.TabIndex = 1;
            this.button1.Text = "Загрузка из файла";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(352, 273);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Подчиненное приложение";
            this.ResumeLayout(false);

        }
        #endregion

        /// 
        /// The main entry point for the application.
        /// 
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                bool CreatedNew;
                // Создаём мьютекс
                mutex = new Mutex(true, "MyMutex", out CreatedNew);
                // Проверяем не создавался ли он раньше
                if (CreatedNew == true)
                    throw new Exception("Не загружалось главное приложение !!!");

                // Ожидаем перехода мьютекса в сигнальное состояние
                MessageBox.Show("Начинаем ожидать перехода в сигнальное состояние", "Информация");
                mutex.WaitOne();
                MessageBox.Show("Мьютекс свободен !!!", "Информация");
                // Открываем файл и получаем доступ на чтение
                file = new FileInfo("C:\\data.txt");
                stream = file.OpenText();
                // Заполняем текстовое поле
                data.Text = stream.ReadToEnd();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}