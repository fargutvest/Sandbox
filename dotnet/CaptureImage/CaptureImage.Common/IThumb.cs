using System.Drawing;

namespace CaptureImage.Common
{
    public interface IThumb
    {
        Rectangle[] HandleRectangles { get; }
        bool Visible { get; set; }
        Size Size { get; set; }
        Point Location { get; set; }
        void Refresh();

        void HideExtra();
        void ShowExtra();
    }
}
