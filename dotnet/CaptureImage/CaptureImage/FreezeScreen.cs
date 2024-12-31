using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptureImage
{
    public partial class FreezeScreen : ScreenBase
    {
        public FreezeScreen(Size clientSize, Point location) : base(clientSize, location)
        {
            InitializeComponent();
        }
    }
}
