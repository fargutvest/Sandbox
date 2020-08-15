using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.IO;
using System.Net.Sockets;
namespace Master
{
    /// 
    /// Summary description for Form1.
    /// 
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

        // Переменная - мьютекс
        private Mutex mutex;
        private FileInfo file;
        // С помощью данной переменной мы будем записываться в файл
        private StreamWriter stream;
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
            try
            {
                bool CreatedNew;
                // Создаём мьютекс
                mutex = new Mutex(true, "MyMutex", out CreatedNew);
                // Проверяем не создавался ли он раньше
                if (CreatedNew == false)
                    throw new Exception("Такой мьютекс уже существует !!!");
                // Создаём файл и получаем доступ на запись
                file = new FileInfo("C:\\data.txt");
                stream = file.CreateText();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 88);
            this.button1.TabIndex = 0;
            this.button1.Text = "Заполнять файл";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(208, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 88);
            this.button2.TabIndex = 1;
            this.button2.Text = "Разблокировать файл";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(360, 117);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Главное приложение";
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
                char ch;
                // Генерируем случайные буквы
                Random rnd = new Random();
                for (int i = 0; i < 10; i++)
                {
                    ch = (char)rnd.Next('A', 'Z');
                    // Записываем в файл новую букву
                    stream.Write(ch);
                }

                stream.Write("\r\n");
                // Сбрасываем буффер
                stream.Flush();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            // Переводим мьютекс в сигнальное состояние и конечно же закрываем файл
            stream.Close();
            mutex.ReleaseMutex();
        }
    }
}