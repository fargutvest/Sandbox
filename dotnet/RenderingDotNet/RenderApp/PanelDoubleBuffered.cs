using System.Windows.Forms;

namespace RenderApp
{
    public class PanelDoubleBuffered : Panel
    {
        public PanelDoubleBuffered()
            : base()
        {
            this.DoubleBuffered = true;
        }
    }
}
