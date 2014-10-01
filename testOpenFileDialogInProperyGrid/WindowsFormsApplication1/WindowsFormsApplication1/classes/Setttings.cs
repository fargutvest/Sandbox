using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApplication1
{
    public class Settings
    {
        string _path;
        //[Editor(typeof(FilteredFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Editor(typeof(Adani.FilteredFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        [EditorAttribute(typeof(System.Windows.Forms.Design.), typeof(System.Drawing.Design.UITypeEditor))]
        public string Path1
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
    }
}
