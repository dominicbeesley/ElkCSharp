using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ElkHWLib
{
    public unsafe class ULA
    {
        //our own copy of ram
        private byte[] _ram = new byte[32768];

        private readonly byte[] char_scanlines_per_mode = new byte[] { 8, 8, 8, 10, 8, 8, 10, 10 };
        private readonly ushort[] modelens_per_mode = new ushort[] { 0x5000, 0x5000, 0x5000, 0x4000, 0x2800, 0x2800, 0x2000, 0x2000 };
        private readonly ushort[] mode_end_scanline_per_mode = new ushort[] { 256, 256, 256, 250, 256, 256, 250, 250 };

        public int CharScanLine { get; private set; }
        public int ScreenX { get; private set; }
        public int ScreenY { get; private set; }

        public int Mode { get; private set; }

        public int CurModeCharScanLines { get; private set; }
        public ushort CurModeModeLen { get; private set; }
        public int CurModeEndY { get; private set; }

        public ushort ScreenStart { get; private set; }

        public ushort CurAddr { get; private set; }
        public ushort CurCharRowAddr { get; private set; }

        public ushort CurModeBytesPerCharRow { get; private set; }

        public Bitmap ScreenBitmap { get; }

        public bool OddNotEven { get; private set; }


        BitmapData bitMapData;

        byte* curbmpdata;

        public ULA()
        {
            ScreenBitmap = new Bitmap(640, 256, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            ColorPalette pal = ScreenBitmap.Palette;
            // this is the _physical_ palette, the logical to physical mapping is done in the rasterizer
            for (int i = 0; i < 256; i++)
            {
                pal.Entries[i] = Color.FromArgb(
                    ((i & 1) != 0) ? 255 : 0,
                    ((i & 2) != 0) ? 255 : 0,
                    ((i & 4) != 0) ? 255 : 0
                    );
            }
            ScreenBitmap.Palette = pal;

            SetMode(0);

            ScreenStart = 0x3000;

            bitMapData = ScreenBitmap.LockBits(new Rectangle(Point.Empty, ScreenBitmap.Size), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            curbmpdata = (byte *)bitMapData.Scan0;
        }

        private void SetMode(int mode)
        {
            Mode = mode;
            CurModeCharScanLines = char_scanlines_per_mode[mode];
            CurModeEndY = mode_end_scanline_per_mode[mode];
            CurModeModeLen = modelens_per_mode[mode];
            CurModeBytesPerCharRow = ((mode & 4) > 0)?(ushort)320:(ushort)640;
        }

        internal byte ReadReg(int v)
        {
            switch (v & 0xF)
            {
                default:
                    return 0;
            }
        }

        public void RamWrite(ushort addr, byte val)
        {
            _ram[addr & 0x7FFF] = val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cpu_addr"></param>
        /// <returns>True if cpu can execute this tick</returns>
        public bool tick(ushort cpu_addr)
        {
            //TODO: Mode 0 only!
            if (ScreenX <= 640 - 8 && ScreenY < CurModeEndY)
            {
                if (CharScanLine < 8)
                {
                    byte val = _ram[CurAddr];
                    for (int i = 0; i < 8; i++)
                    {
                        curbmpdata[ScreenX + i] = ((val & 0x80) != 0) ? (byte)7 : (byte)0;
                        val = (byte)(val << 1);
                    }
                    CurAddr += 8;
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        curbmpdata[ScreenX + i] = 0;
                    }
                }
            }

            ScreenX += 8;
            if (ScreenX > 1024)
            {
                ScreenX = 0;
                ScreenY++;
                if (ScreenY > ((OddNotEven) ? 312 : 313))
                {
                    ScreenY = 0;
                    OddNotEven = !OddNotEven;
                    curbmpdata = (byte*)bitMapData.Scan0;
                    CurCharRowAddr = CurAddr = ScreenStart;
                    CharScanLine = 0;
                }
                else
                {

                    // next scan line
                    CharScanLine++;
                    curbmpdata += bitMapData.Stride;
                    if (CharScanLine >= CurModeCharScanLines)
                    {
                        //next char row
                        CharScanLine = 0;

                        CurCharRowAddr += CurModeBytesPerCharRow;
                        if (CurCharRowAddr >= 0x8000)
                        {
                            CurCharRowAddr -= CurModeModeLen;
                        }
                        CurAddr = CurCharRowAddr;
                    } else
                    {
                        CurAddr = (ushort)(CurCharRowAddr + CharScanLine);
                    }
                }
            }

            return true;
        }

    }
}
