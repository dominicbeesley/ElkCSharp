﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using UEFLib;

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

        public enum CassetteMode { Input = 0, Sound = 1, Output = 2, None = 3 }

        private byte _isr;
        private byte _ier;
        private ushort _cas_shr;       
        private CassetteMode _cas_mode;


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

        public EventHandler IRQChange;


        BitmapData bitMapData;

        byte* curbmpdata;
        private bool disposedValue;

        public bool ROM_External { get; private set; }
        public byte ROM_IntBank { get; private set; }
        public byte ROM_ExtBank { get; private set; }

        public bool IRQ { get { return (_isr & ISR_MASK_MASTER) != 0; } }

        public bool CapsLock { get; private set; }
        public bool Motor { get; private set; }
        public UEFTapeStreamer UEF { get; set; }
        
        public ushort HiToneDetect { get; private set; }
        public ushort LoToneDetect { get; private set; }

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
            curbmpdata = (byte*)bitMapData.Scan0;

            Reset(true);
        }

        private void SetMode(int mode)
        {
            Mode = mode;
            CurModeCharScanLines = char_scanlines_per_mode[mode];
            CurModeEndY = mode_end_scanline_per_mode[mode];
            CurModeModeLen = modelens_per_mode[mode];
            CurModeBytesPerCharRow = ((mode & 4) > 0) ? (ushort)320 : (ushort)640;
        }

        public void Reset(bool hard)
        {

            if (hard)
                _isr = ISR_MASK_RESET | ISR_MASK_NOTUSED;
            else
                _isr = ISR_MASK_NOTUSED;

            _ier = 0;

            //not sure what the actual reset state is of these - this is a guess!
            ROM_External = false;
            ROM_ExtBank = ROM_IntBank = 10;

            SetMode(0);
            CurAddr = 0;
        }

        protected void UpdateInterrupts()
        {
            if ((_isr & _ier & 0x7C) != 0)
            {
                //irq on
                _isr |= ISR_MASK_MASTER;                
            } 
            else
            {
                _isr &= (byte)(ISR_MASK_MASTER ^ 0xFF);
            }
            if (IRQChange != null)
                IRQChange(this, EventArgs.Empty);
        }

        public bool ReadReg(ushort addr, out byte dat)
        {
            switch (addr & 0xF)
            {
                case 0:
                    dat = ReadRegISR();
                    return true;

                case 4:
                    dat = (byte)_cas_shr;
                    _isr &= (byte)(ISR_MASK_RXFULL ^ 0xFF);
                    UpdateInterrupts();
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

        public void SyncRAM(byte[] ram)
        {
            ram.CopyTo(_ram, 0);
        }


        public void WriteReg(ushort addr, byte val)
        {
            switch (addr & 0xF)
            {
                case 0:
                    _ier = (byte)(val & 0x7E);
                    UpdateInterrupts();
                    break;
                case 2:
                    ScreenStart = (ushort)((ScreenStart & 0x7E00) | ((val & 0xE0) << 1));
                    break;
                case 3:
                    ScreenStart = (ushort)((ScreenStart & 0x01E0) | ((val & 0x3F) << 9));
                    break;
                case 4:
                    _cas_shr = val;
                    _isr &= (byte)(ISR_MASK_TXEMPTY ^ 0xFF);
                    UpdateInterrupts();
                    break;
                case 5:
                    // clear interrupts
                    if ((val & 0x70) != 0)
                    {
                        if ((val & 0x10) != 0)
                            _isr &= (byte)(ISR_MASK_DISPEND ^ 0xFF);
                        if ((val & 0x20) != 0)
                            _isr &= (byte)(ISR_MASK_RTC ^ 0xFF);
                        if ((val & 0x40) != 0)
                            _isr &= (byte)(ISR_MASK_TONE_DETECT ^ 0xFF);
                        UpdateInterrupts();
                    }

                    //rom faffery - more or less lifted from Elkulator
                    ROM_ExtBank = (byte)(val & 0x0F);
                    if (ROM_ExtBank >= 0xC)
                    {
                        ROM_External = true;
                    }
                    else if ((val & 0xC) == 0x8)
                    {
                        ROM_External = false;
                        ROM_IntBank = ROM_ExtBank;
                    }


                    break;

                case 7:
                    SetMode((val & 0x38) >> 3);
                    CapsLock = (val & 0x80) != 0;
                    Motor = (val & 0x40) != 0;
                    break;

            }
        }

        private byte vduval;

        private int uefTicks = 0;
        private int hiToneTicks = 0;
        private int cas_bits_left = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if cpu can execute this tick from RAM - in effect a 1MHz clock stretched during hires modes</returns>
        public bool Tick()
        {
            bool ret = (ScreenX & 8) != 0;

            //TODO: Palette

            if (ScreenX <= 640 - 8 && ScreenY < CurModeEndY)
            {
                if (CharScanLine < 8)
                {
                    if (CurAddr >= 0x8000)
                        CurAddr = (ushort)(CurAddr - CurModeModeLen);

                    if (Mode == 0 || Mode == 3)
                    {
                        vduval = _ram[CurAddr];
                        ret = false;

                        for (int i = 0; i < 8; i++)
                        {
                            curbmpdata[ScreenX + i] = ((vduval & 0x80) != 0) ? (byte)7 : (byte)0;
                            vduval = (byte)(vduval << 1);
                        }
                        CurAddr += 8;
                    }
                    else if (Mode == 1)
                    {
                        vduval = _ram[CurAddr];
                        ret = false;

                        for (int i = 0; i < 4; i++)
                        {
                            byte c;
                            switch(vduval & 0x88)
                            {
                                case 0x88:
                                    c = 0x7;
                                    break;
                                case 0x80:
                                    c = 0x3;
                                    break;
                                case 0x08:
                                    c = 0x1;
                                    break;
                                default:
                                    c = 0;
                                    break;
                            }
                            curbmpdata[ScreenX + i * 2] = c;
                            curbmpdata[ScreenX + i * 2 + 1] = c;
                            vduval = (byte)(vduval << 1);
                        }
                        CurAddr += 8;
                    }
                    else if (Mode == 2)
                    {
                        vduval = _ram[CurAddr];
                        ret = false;

                        for (int i = 0; i < 2; i++)
                        {
                            byte c = (byte)(
                                ((vduval & 0x80) >> 4) |
                                ((vduval & 0x20) >> 3) |
                                ((vduval & 0x08) >> 2) |
                                ((vduval & 0x02) >> 1)
                                );

                            curbmpdata[ScreenX + i * 4] = c;
                            curbmpdata[ScreenX + i * 4 + 1] = c;
                            curbmpdata[ScreenX + i * 4 + 2] = c;
                            curbmpdata[ScreenX + i * 4 + 3] = c;
                            vduval = (byte)(vduval << 1);
                        }
                        CurAddr += 8;
                    }
                    else if (Mode == 5)
                    {
                        if ((ScreenX & 8) == 0)
                        {
                            vduval = _ram[CurAddr];
                            CurAddr += 8;
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            byte c;
                            switch (vduval & 0x88)
                            {
                                case 0x88:
                                    c = 0x7;
                                    break;
                                case 0x80:
                                    c = 0x3;
                                    break;
                                case 0x08:
                                    c = 0x1;
                                    break;
                                default:
                                    c = 0;
                                    break;
                            }
                            curbmpdata[ScreenX + i * 4] = c;
                            curbmpdata[ScreenX + i * 4 + 1] = c;
                            curbmpdata[ScreenX + i * 4 + 2] = c;
                            curbmpdata[ScreenX + i * 4 + 3] = c;
                            vduval = (byte)(vduval << 1);
                        }
                    }
                    else //(Mode == 4 || Mode == 6 || Mode == 7)
                    {
                        if ((ScreenX & 8) == 0)
                        {
                            vduval = _ram[CurAddr];
                            CurAddr += 8;
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            byte c = ((vduval & 0x80) != 0) ? (byte)7 : (byte)0;
                            curbmpdata[ScreenX + i * 2] = c;
                            curbmpdata[ScreenX + i * 2 + 1] = c;
                            vduval = (byte)(vduval << 1);
                        }
                    }

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
            if (ScreenX >= 1024)
            {
                ScreenX = 0;
                ScreenY++;
                if (ScreenY == 99)
                {
                    _isr |= ISR_MASK_RTC;
                    UpdateInterrupts();
                } else if (ScreenY == CurModeEndY)
                {
                    _isr |= ISR_MASK_DISPEND;
                    UpdateInterrupts();
                }
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
                        CurAddr = CurCharRowAddr;
                    }
                    else
                    {
                        CurAddr = (ushort)(CurCharRowAddr + CharScanLine);
                    }
                }
            }

            if (uefTicks-- < 0)
            {
                HiToneDetect -= (ushort)(HiToneDetect >> 8);
                LoToneDetect -= (ushort)(LoToneDetect >> 8);
                uefTicks = 1666;
                if (UEF != null && _cas_mode == CassetteMode.Input && Motor)
                {
                    UEFTapeBit bit = UEF.Tick();

                    if (bit == UEFTapeBit.LowTone)
                        LoToneDetect += 255;
                    else if (bit == UEFTapeBit.HighTone)
                        HiToneDetect += 255;

                    //read a bit from the cassette
                    if (cas_bits_left > 0)
                    {
                        _cas_shr = (ushort)((_cas_shr >> 1) | ((bit == UEFTapeBit.LowTone) ? 0 : 0x100));
                        
                        cas_bits_left--;
                        if (cas_bits_left == 0)
                        {
                            _isr |= ISR_MASK_RXFULL;
                            UpdateInterrupts();
                        }
                    } else
                    {
                        if (bit == UEFTapeBit.HighTone)
                        {
                            hiToneTicks++;
                            if (hiToneTicks == 60)
                            {
                                _isr |= ISR_MASK_TONE_DETECT;
                                UpdateInterrupts();
                            }
                        } else
                        {
                            hiToneTicks = 0;
                            if (bit == UEFTapeBit.LowTone)
                            {
                                cas_bits_left = 9;
                            }
                        }
                    }
                } 
            }

            return ret;
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
