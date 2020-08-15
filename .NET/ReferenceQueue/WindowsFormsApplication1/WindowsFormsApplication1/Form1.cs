using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Queue<string> queue = new Queue<string>();
        public Form1()
        {
            InitializeComponent();
            queue.Enqueue("noteForm1");
            a _a = new a(queue);
        }
    }

    public class a
    {
        Queue<string> refQueue;
        public a(Queue<string> _queue)
        {
            refQueue = _queue;
            Enqueue();
        }

        void Enqueue()
        {
            refQueue.Enqueue("note_a");
        }
    }
}
