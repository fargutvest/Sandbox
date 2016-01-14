using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;


namespace testPsevdoConsole
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
            // предопределяем класс, назначаем вывод через него
            Console.SetOut(RichTextBoxWriter.Instance);
            Console.SetIn(RichTextBoxReader.Instance);
            

            // говорим куда выводить
            RichTextBoxWriter.Instance.OutputBox = richTextBox1;
            RichTextBoxReader.Instance.InputBox = richTextBox1;
            // далее просто пишем
            Console.WriteLine("Hello Word!");
            
            string s  = Console.ReadLine();
        }
    }

    class RichTextBoxWriter : StreamWriter
    {
        public static RichTextBoxWriter Instance = new RichTextBoxWriter();

        public RichTextBox OutputBox;

        public RichTextBoxWriter(): base(new FileStream(Assembly.GetExecutingAssembly().GetName().Name + ".log", FileMode.Create))
        {
            this.AutoFlush = true;

            if (base.BaseStream.Position != 0)
            {
                base.WriteLine();
                base.WriteLine();
            }
        }

        public override void WriteLine(string value)
        {
            _Write(value + "\r\n");
        }

        public override void WriteLine(string format, params object[] arg)
        {
            _Write(String.Format(format, arg) + "\r\n");
        }

        public override void WriteLine()
        {
            _Write("\r\n");
        }

        public override void Write(string value)
        {
            _Write(value);
        }

        public override void Write(string format, params object[] arg)
        {
            _Write(String.Format(format, arg));
        }

        // Generic Write Method

        private void _Write(string text)
        {
            if (!OutputBox.IsDisposed)
            {
                Action f = () =>
                {
                    OutputBox.AppendText(text);
                    //OutputBox.ScrollToBottom();
                };

                if (OutputBox.InvokeRequired)
                    // Не ждем завершение UI операции.
                    OutputBox.BeginInvoke(f);
                else
                    f();
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                base.Write(String.Format("[{0:yyyy.MM.dd HH:mm:ss.ffff}] {1}", DateTime.Now, text));
            }
        }
    }
    class RichTextBoxReader : StreamReader
    {
        public static RichTextBoxReader Instance = new RichTextBoxReader();

        public RichTextBox InputBox;

         public RichTextBoxReader(): base(new FileStream(Assembly.GetExecutingAssembly().GetName().Name + ".log", FileMode.Create))
        {
            
        }
    }
}
