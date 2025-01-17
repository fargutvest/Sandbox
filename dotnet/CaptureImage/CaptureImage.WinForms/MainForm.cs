using System.Windows.Forms;

namespace CaptureImage.WinForms
{
    public partial class MainForm : Form
    {
        private AppContext appContext;

        public MainForm(AppContext appContext)
        {
            InitializeComponent();
            this.appContext = appContext;
        }

        protected override void WndProc(ref Message m)
        {
            appContext.hotKeysHelper.WndProc(ref m);
            base.WndProc(ref m);
        }
    }
}
