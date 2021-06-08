using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ElkHWLib
{
    public unsafe class ULA : IDisposable
    {
        public const byte ISR_MASK_MASTER = 0x01;
        public const byte ISR_MASK_RESET = 0x02;
        public const byte ISR_MASK_DISPEND = 0x04;
        public const byte ISR_MASK_RTC = 0x08;
        public const byte ISR_MASK_RXFULL = 0x10;
        public const byte ISR_MASK_TXEMPTY = 0x20;
        public const byte ISR_MASK_TONE_DETECT = 0x40;
        public const byte ISR_MASK_NOTUSED = 0x80;

        private byte _isr;
        private byte _ier;
        private byte _cas_shr;


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
        private bool disposedValue;

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

            bitMapData = ScreenBitmap.LockBits(new Rectangle(Point.Empty, ScreenBitmap.Size), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            curbmpdata = (byte *)bitMapData.Scan0;

            Reset();
        }

        private void SetMode(int mode)
        {
            Mode = mode;
            CurModeCharScanLines = char_scanlines_per_mode[mode];
            CurModeEndY = mode_end_scanline_per_mode[mode];
            CurModeModeLen = modelens_per_mode[mode];
            CurModeBytesPerCharRow = ((mode & 4) > 0)?(ushort)320:(ushort)640;
        }

        public void Reset()
        {
            SetMode(0);

            ScreenStart = 0x3000;

            _isr = ISR_MASK_RESET | ISR_MASK_NOTUSED;
        }

        public bool ReadReg(ushort addr, out byte dat)
        {
            switch (addr & 0xF)
            {
                case 0:
                    dat = ReadRegISR();
                    return true;

                case 4:
                    dat = _cas_shr;
                    return true;

                default:
                    dat = 0xFF;
                    return false;
            }
        }

        protected byte ReadRegISR()
        {
            byte ret = _isr;
            _isr &= ISR_MASK_RESET ^ 0xFF;
            return ret;
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
                        curbmpdata[ScreenX + i] = 4;
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ScreenBitmap?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ULA()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
