using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace testTraceListener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Trace.AutoFlush = true;
            Trace.IndentSize = 4;
            TraceListener tl = new TextWriterTraceListener("test.log");
            tl.TraceOutputOptions = TraceOptions.ThreadId;
            Trace.Listeners.Add(tl);
            Trace.Listeners.Remove("");
        }

        void test()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = new Task(execute, cts.Token);
            task.ContinueWith(continoution);
            task.Start();
        }

        void continoution(Task antecendet)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test();
        }

        void execute(object cancel)
        {
            CancellationToken ct = (CancellationToken)cancel;
            int y = 0;
            while (true)
            {
                y++;
                Trace.TraceError("sssssssssssssssssssssssss" + y, new object[]{"dsfsf"});
               // Trace.TraceInformation("inf inf inf inf inf"+ y*3);
                //Trace.TraceWarning("warng warng warng" + y * 7);
            }
        }


    }
}
