using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication10
{
    

    public partial class MyForm : Form
    {
        MyClass _myClass;
        private static string FileLog = "richTextBox1.txt";
        public MyForm()
        {
            InitializeComponent();
            _myClass = new MyClass(this);
            buttonStart.MouseClick +=new MouseEventHandler(buttonStart_MouseClick);
        }

        void buttonStart_MouseClick(object sender, EventArgs e)
        {
            
        }

        private void SetText(string text)
        {
            richTextBox1.AppendText(text + "\n");

            try
            {
                File.AppendAllText(FileLog, text + "\n");
            }
            catch { }
        }

        public void SetTextSafe(string value)
        {
            {
                if (richTextBox1.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    Invoke(d, new object[] { value + " (Invoke)" });
                }
                else
                {
                    SetText(value + " (No Invoke)"); // It's on the same thread, no need for Invoke
                }
            }
        }
      
        

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _myClass.StartTask();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _myClass.StopTask(_myClass.MyTask, _myClass.cts);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_myClass is IDisposable)
            {
                _myClass.Dispose();
            }
        }

       public void deleteFileLog()
        {
            try
            {
                File.Delete(FileLog);
            }
            catch { }
        }

    }
}
