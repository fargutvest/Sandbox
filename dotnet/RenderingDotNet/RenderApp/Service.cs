using RenderingCommon;
using System.Windows.Forms;

namespace RenderApp
{
    class Service : IContract
    {
        private Form1 form1 = (Form1)Application.OpenForms["Form1"];

        public void NewVerticalLine(int[] generatedVerticalLine)
        {
            form1.ProcessLine(generatedVerticalLine);
        }
    }
}
