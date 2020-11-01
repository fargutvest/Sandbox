using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Controls;

namespace WPF_ScreenSaver
{
    /// <summary>
    /// Global variables
    /// </summary>
    public class Globals
    {
        #region Data
        public static IList<FileInfo> Files = new List<FileInfo>();
        public static String TempFileName = "ScreenSaverPictureLocations.txt";
        public static IList<String> WorkingSetOfImages = new List<String>();
        public readonly static Int32 WorkingSetLimit = 15;
        #endregion
    }
}
