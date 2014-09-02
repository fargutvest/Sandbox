using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Adani
{
    public static class ExceptionLog
    {
        public static event EventHandler ExceptionEvent;
        public static void log(Exception ex)
        {
            string str = string.Format("{0:HH:mm:ss:ms} {1},{2},{3}", DateTime.Now, ex.TargetSite, ex.Source, ex.Message);
            Debug.WriteLine(str);
            try
            {
              // if (SettingsDetector.Instance.Logging)
              // {
              //     File.AppendAllText("log.txt", str + "\r\n");
              // }   
            } //
            catch (Exception) { }

            if (ExceptionEvent != null)
            {
                ExceptionEvent(ex,null);
            }
        }
        public static void log(string s)
        {
            string str = string.Format("{0:HH:mm:ss:ms} {1}", DateTime.Now, s);
            Debug.WriteLine(str);
            try
            {
               // if (SettingsDetector.Instance.Logging)
               // {
               //     File.AppendAllText("log.txt", str + "\r\n");
               // }
            }
            catch (Exception) { }

        }
    }
}

