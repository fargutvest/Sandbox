using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Task _task;

        private void BodyTest()
        {
            Test1();
        }

        private async void Test1()
        {
            var taskSource = new TaskCompletionSource<bool>();
            Debug.WriteLine(taskSource.Task.Status);
            taskSource.SetResult(false);
            Debug.WriteLine(taskSource.Task.Status);
            await taskSource.Task;
            Debug.WriteLine(taskSource.Task.Status);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            BodyTest();
        }
    }
}
