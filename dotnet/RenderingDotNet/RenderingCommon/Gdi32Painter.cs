using System;
using System.Runtime.InteropServices;

namespace RenderingCommon
{
    public class Gdi32Painter : IDisposable
    {
        [DllImport("gdi32")]
        private extern static int SetDIBitsToDevice(HandleRef hDC, int xDest, int yDest, int dwWidth,
            int dwHeight, int XSrc, int YSrc, int uStartScan, int cScanLines, ref int lpvBits, ref BITMAPINFO lpbmi, uint fuColorUse);

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFOHEADER
        {
            public int bihSize;
            public int bihWidth;
            public int bihHeight;
            public short bihPlanes;
            public short bihBitCount;
            public int bihCompression;
            public int bihSizeImage;
            public double bihXPelsPerMeter;
            public double bihClrUsed;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFO
        {
            public BITMAPINFOHEADER biHeader;
            public int biColors;
        }

        private HandleRef hDCRef;

        public Gdi32Painter(HandleRef hDCRef)
        {
            this.hDCRef = hDCRef;
        }

        ~Gdi32Painter()
        {
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }



        public void Paint(int[] frame, int width, int height)
        {
            BITMAPINFO bitmapInfo = new BITMAPINFO
            {
                biHeader =
                {
                    bihBitCount = 32,
                    bihPlanes = 1,
                    bihSize = 40,
                    bihWidth = width,
                    bihHeight = -height,
                    bihSizeImage = (width * height) << 2
                }
            };

            SetDIBitsToDevice(hDCRef, 0, 0, width, height, 0, 0, 0, height, ref frame[0], ref bitmapInfo, 0);
        }
    }
}
