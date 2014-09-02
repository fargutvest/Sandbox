using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Detector
{
    public static class ExceptionLog
    {
        public static void log(Exception ex)
        {
            Debug.WriteLine(string.Format("{0},{1},{2}", ex.TargetSite, ex.Source, ex.Message));
            System.Windows.Forms.MessageBox.Show(ex.Message);
        }
    }
}
