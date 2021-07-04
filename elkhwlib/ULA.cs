using System;
using System.Drawing;
using System.Drawing.Imaging;
using UEFLib;

namespace ElkHWLib
{
    public unsafe class ULA
    {

        /// <summary>
        /// Logical to physical colour translation, this indexes each of the 16 logical colours to
        /// a physical 3 bit colour (in BGR order)
        /// 
        /// <list>
        ///     <item>In 2 colour modes indexes 0, 8 are used</item>
        ///     <item>In 4 colour modes indexes 0, 2, 8, 10 are used</item>
        ///     <item>In 16 colour modes all indexes map naturally</item>
        /// </list>
        /// 
        /// 
        /// 
        /// </summary>
        public byte[] Palette { get; init; }

        /// <summary>
        /// Logical to physical mapping to Bgr32 
        /// </summary>
        public Int32[] Palette32 { get; init; }

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

        /// <summary>
        /// Scan line within the current character row
        /// </summary>
        public int CharScanLine { get; private set; }
        /// <summary>
        /// The raster X position, possibly outside the screen bitmap. The screen bitmap is always 640 pixels wide, each scan line is 1024 pixels. ScreenX increases by 8 on each 2MHz tick
        /// </summary>
        public int ScreenX { get; private set; }
        /// <summary>
        /// The raster Y position, possibly outside the screen bitmap. The screen bitmap is always 256 pixels high, each frame is either 312 or 313 pixels. ScreenY increases every 128 2MHz ticks
        /// </summary>
        public int ScreenY { get; private set; }

        /// <summary>
        /// Current screen mode
        /// </summary>
        public int Mode { get; private set; }

        /// <summary>
        /// Number of scan lines in a character row, 8 in normal modes, 10 in Venetian (modes 3, 6)
        /// </summary>
        public int CurModeCharScanLines { get; private set; }

        /// <summary>
        /// The current screen mode length in bytes 
        /// </summary>
        public ushort CurModeModeLen { get; private set; }

        /// <summary>
        /// The last displayed line in the current screen mode
        /// </summary>
        public int CurModeEndY { get; private set; }

        /// <summary>
        /// The start address of the screen, which will wrap when it passes 0x8000
        /// </summary>
        public ushort ScreenStart { get; private set; }

        /// <summary>
        /// The current raster screen address
        /// </summary>
        public ushort CurAddr { get; private set; }

        /// <summary>
        /// The current raster screen address of the current character row
        /// </summary>
        public ushort CurCharRowAddr { get; private set; }

        /// <summary>
        /// The number of bytes in a character row in the current mode
        /// </summary>
        public ushort CurModeBytesPerCharRow { get; private set; }

        /// <summary>
        /// The screen bitmap, this is a bitmap in a top-left to bottom-right (raster) order. Each byte should hold 0-7 for each pixel. The stride is 640 and there are 256 rows.
        /// </summary>
        public Int32[] ScreenData { get; init; }
        /// <summary>
        /// Current index into the ScreenData bitmap
        /// </summary>
        public int screenDataIX { get; private set; }

        /// <summary>
        /// Whether the current frame is Odd (312 lines) or Even (313 lines)
        /// </summary>
        public bool OddNotEven { get; private set; }

        /// <summary>
        /// This event fires when the IRQ status of the ULA changes
        /// </summary>
        public EventHandler IRQChange;

        /// <summary>
        /// 0x8000-0xBFFF refers to an external ROM
        /// </summary>
        public bool ROM_External { get; private set; }
        /// <summary>
        /// The index for internal ROM/keyboard
        /// </summary>
        public byte ROM_IntBank { get; private set; }
        /// <summary>
        /// The index for external ROM
        /// </summary>
        public byte ROM_ExtBank { get; private set; }

        /// <summary>
        /// The ULA's IRQ status (true is IRQ asserted)
        /// </summary>
        public bool IRQ { get { return (_isr & ISR_MASK_MASTER) != 0; } }

        /// <summary>
        /// Caps Lock light
        /// </summary>
        public bool CapsLock { get; private set; }

        /// <summary>
        /// Cassette motor active
        /// </summary>
        public bool Motor { get; private set; }

        /// <summary>
        /// Can be set to A UEF tape stream
        /// </summary>
        public UEFTapeStreamer UEF { get; set; }

        /// <summary>
        /// Indication that high tones have been read from a tape - this is a crudely low-pass filtered indication
        /// </summary>
        public ushort HiToneDetect { get; private set; }
        /// <summary>
        /// Indication that low tones have been read from a tape - this is a crudely low-pass filtered indication
        /// </summary>
        public ushort LoToneDetect { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ULA()
        {
            ScreenData = new Int32[640 * 256];
            Palette = new byte[16];
            Palette32 = new int[16];

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


        /// <summary>
        /// Reset the ULA
        /// </summary>
        /// <param name="hard">When true, sets the reset flag as if power had been reset rather than break pressed.</param>
        public void Reset(bool hard)
        {

            if (hard)
                _isr = ISR_MASK_RESET | ISR_MASK_NOTUSED;
            else
                _isr = ISR_MASK_NOTUSED;

            _ier = 0;

            //not sure what the actual reset state is of these - this is a guess!
            ROM_External = false;
            ROM_ExtBank = ROM_IntBank = Elk.ROMNO_BASIC;

            SetMode(0);
            CurAddr = 0;
            screenDataIX = 0;

            //reset palette - not sure what the ULA actually gets reset to!?
            for (byte i = 0; i < 15; i++)
            {
                Palette[i] = i;
                Palette32[i] = ToPalette32(i);
            }
        }

        Int32 ToPalette32(byte b)
        {
            return ((b & 1) != 0 ? 0xFF0000 : 0) + ((b & 2) != 0 ? 0xFF00 : 0) + ((b & 4) != 0 ? 0xFF : 0);
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

        /// <summary>
        /// Performs a register read
        /// </summary>
        /// <param name="addr">The address to read from (only the lower nybble is inspected, addresses wrap)</param>
        /// <param name="dat">The data that is read if any</param>
        /// <returns>true if data returned, if false the emulation should leave the CPU's data bus set to its previous value</returns>
        /// <remarks>Note: reading registers has side-effects (i.e. clearing ISR), for displaying register values in a debugger another method should be used</remarks>
        /// 
        public bool ReadReg(ushort addr, ref byte dat)
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
                    return false;
            }
        }

        protected byte ReadRegISR()
        {
            byte ret = _isr;
            _isr &= ISR_MASK_RESET ^ 0xFF;
            return ret;
        }

        /// <summary>
        /// Write the ULA's copy of RAM
        /// </summary>
        /// <param name="addr">address to write to (wraps to lowest 15 bits)</param>
        /// <param name="val">data to write</param>
        /// <remarks>The ula holds its own copy of ram, this must be kept in sync with the CPU's copy</remarks>
        public void RamWrite(ushort addr, byte val)
        {
            _ram[addr & 0x7FFF] = val;
        }

        /// <summary>
        /// update the local copy of ram from the passed byte array
        /// </summary>
        /// <param name="ram"></param>
        public void SyncRAM(byte[] ram)
        {
            ram.CopyTo(_ram, 0);
        }


        /// <summary>
        /// Write to a register
        /// </summary>
        /// <param name="addr">The address to read from (only the lower nybble is inspected, addresses wrap)</param>
        /// <param name="val">The data to write</param>
        /// <remarks>Note: writing to register using this function has side effects</remarks>
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

                case 8:
                    val = (byte)~val;
                    Palette32[0] = ToPalette32(Palette[0] = (byte)((Palette[0] & 0x3) | ((val & 0x10) >> 2)));
                    Palette32[2] = ToPalette32(Palette[2] = (byte)((Palette[2] & 0x3) | ((val & 0x20) >> 3)));
                    Palette32[8] = ToPalette32(Palette[8] = (byte)((Palette[8] & 0x1) | ((val & 0x40) >> 4) | ((val & 0x04) >> 1)));
                    Palette32[10] = ToPalette32(Palette[10] = (byte)((Palette[10] & 0x1) | ((val & 0x80) >> 5) | ((val & 0x08) >> 2)));
                    break;
                case 9:
                    val = (byte)~val;
                    Palette32[0] = ToPalette32(Palette[0] = (byte)((Palette[0] & 0x4) | ((val & 0x10) >> 3) | (val & 0x01)));
                    Palette32[2] = ToPalette32(Palette[2] = (byte)((Palette[2] & 0x4) | ((val & 0x20) >> 4) | ((val & 0x02) >> 1)));
                    Palette32[8] = ToPalette32(Palette[8] = (byte)((Palette[8] & 0x6) | ((val & 0x04) >> 2)));
                    Palette32[10] = ToPalette32(Palette[10] = (byte)((Palette[10] & 0x6) | ((val & 0x08) >> 3)));
                    break;
                case 10:
                    val = (byte)~val;
                    Palette32[4] = ToPalette32(Palette[4] = (byte)((Palette[4] & 0x3) | ((val & 0x10) >> 2)));
                    Palette32[6] = ToPalette32(Palette[6] = (byte)((Palette[6] & 0x3) | ((val & 0x20) >> 3)));
                    Palette32[12] = ToPalette32(Palette[12] = (byte)((Palette[12] & 0x1) | ((val & 0x40) >> 4) | ((val & 0x04) >> 1)));
                    Palette32[14] = ToPalette32(Palette[14] = (byte)((Palette[14] & 0x1) | ((val & 0x80) >> 5) | ((val & 0x08) >> 2)));
                    break;
                case 11:
                    val = (byte)~val;
                    Palette32[4] = ToPalette32(Palette[4] = (byte)((Palette[4] & 0x4) | ((val & 0x10) >> 3) | (val & 0x01)));
                    Palette32[6] = ToPalette32(Palette[6] = (byte)((Palette[6] & 0x4) | ((val & 0x20) >> 4) | ((val & 0x02) >> 1)));
                    Palette32[12] = ToPalette32(Palette[12] = (byte)((Palette[12] & 0x6) | ((val & 0x04) >> 2)));
                    Palette32[14] = ToPalette32(Palette[14] = (byte)((Palette[14] & 0x6) | ((val & 0x08) >> 3)));
                    break;
                case 12:
                    val = (byte)~val;
                    Palette32[5] = ToPalette32(Palette[5] = (byte)((Palette[5] & 0x3) | ((val & 0x10) >> 2)));
                    Palette32[7] = ToPalette32(Palette[7] = (byte)((Palette[7] & 0x3) | ((val & 0x20) >> 3)));
                    Palette32[13] = ToPalette32(Palette[13] = (byte)((Palette[13] & 0x1) | ((val & 0x40) >> 4) | ((val & 0x04) >> 1)));
                    Palette32[15] = ToPalette32(Palette[15] = (byte)((Palette[15] & 0x1) | ((val & 0x80) >> 5) | ((val & 0x08) >> 2)));
                    break;
                case 13:
                    val = (byte)~val;
                    Palette32[5] = ToPalette32(Palette[5] = (byte)((Palette[5] & 0x4) | ((val & 0x10) >> 3) | (val & 0x01)));
                    Palette32[7] = ToPalette32(Palette[7] = (byte)((Palette[7] & 0x4) | ((val & 0x20) >> 4) | ((val & 0x02) >> 1)));
                    Palette32[13] = ToPalette32(Palette[13] = (byte)((Palette[13] & 0x6) | ((val & 0x04) >> 2)));
                    Palette32[15] = ToPalette32(Palette[15] = (byte)((Palette[15] & 0x6) | ((val & 0x08) >> 3)));
                    break;
                case 14:
                    val = (byte)~val;
                    Palette32[1] = ToPalette32(Palette[1] = (byte)((Palette[1] & 0x3) | ((val & 0x10) >> 2)));
                    Palette32[3] = ToPalette32(Palette[3] = (byte)((Palette[3] & 0x3) | ((val & 0x20) >> 3)));
                    Palette32[9] = ToPalette32(Palette[9] = (byte)((Palette[9] & 0x1) | ((val & 0x40) >> 4) | ((val & 0x04) >> 1)));
                    Palette32[11] = ToPalette32(Palette[11] = (byte)((Palette[11] & 0x1) | ((val & 0x80) >> 5) | ((val & 0x08) >> 2)));
                    break;
                case 15:
                    val = (byte)~val;
                    Palette32[1] = ToPalette32(Palette[1] = (byte)((Palette[1] & 0x4) | ((val & 0x10) >> 3) | (val & 0x01)));
                    Palette32[3] = ToPalette32(Palette[3] = (byte)((Palette[3] & 0x4) | ((val & 0x20) >> 4) | ((val & 0x02) >> 1)));
                    Palette32[9] = ToPalette32(Palette[9] = (byte)((Palette[9] & 0x6) | ((val & 0x04) >> 2)));
                    Palette32[11] = ToPalette32(Palette[11] = (byte)((Palette[11] & 0x6) | ((val & 0x08) >> 3)));
                    break;
            }
        }

        private byte vduval;

        private int uefTicks = 0;
        private int hiToneTicks = 0;
        private int cas_bits_left = 0;

        /// <summary>
        /// Perform a 2MHz tick
        /// </summary>
        /// <returns>True if cpu can execute this tick from RAM - in effect a 1MHz clock stretched during the active part of the raster in hires modes</returns>
        public bool Tick(bool render)
        {
            bool ret = (ScreenX & 8) != 0;

            if (ScreenX <= 640 - 8 && ScreenY < 256)
            {
                if (CharScanLine < 8 && ScreenY < CurModeEndY)
                {
                    if (render)
                    {
                        if (Mode == 0 || Mode == 3)
                        {
                            vduval = getvidram();

                            for (int i = 0; i < 8; i++)
                            {
                                ScreenData[screenDataIX++] = ((vduval & 0x80) != 0) ? Palette32[8] : Palette32[0];
                                vduval = (byte)(vduval << 1);
                            }
                        }
                        else if (Mode == 1)
                        {
                            vduval = getvidram();

                            for (int i = 0; i < 4; i++)
                            {
                                Int32 c;
                                switch (vduval & 0x88)
                                {
                                    case 0x88:
                                        c = Palette32[10];
                                        break;
                                    case 0x80:
                                        c = Palette32[8];
                                        break;
                                    case 0x08:
                                        c = Palette32[2];
                                        break;
                                    default:
                                        c = Palette32[0];
                                        break;
                                }
                                ScreenData[screenDataIX++] = c;
                                ScreenData[screenDataIX++] = c;
                                vduval = (byte)(vduval << 1);
                            }
                        }
                        else if (Mode == 2)
                        {
                            vduval = getvidram();

                            for (int i = 0; i < 2; i++)
                            {
                                Int32 c = Palette32[
                                    ((vduval & 0x80) >> 4) |
                                    ((vduval & 0x20) >> 3) |
                                    ((vduval & 0x08) >> 2) |
                                    ((vduval & 0x02) >> 1)
                                    ];

                                ScreenData[screenDataIX++] = c;
                                ScreenData[screenDataIX++] = c;
                                ScreenData[screenDataIX++] = c;
                                ScreenData[screenDataIX++] = c;
                                vduval = (byte)(vduval << 1);
                            }
                        }
                        else if (Mode == 5)
                        {
                            if ((ScreenX & 8) == 0)
                            {
                                vduval = getvidram();
                            }

                            for (int i = 0; i < 2; i++)
                            {
                                Int32 c;
                                switch (vduval & 0x88)
                                {
                                    case 0x88:
                                        c = Palette32[10];
                                        break;
                                    case 0x80:
                                        c = Palette32[8];
                                        break;
                                    case 0x08:
                                        c = Palette32[2];
                                        break;
                                    default:
                                        c = Palette32[0];
                                        break;
                                }
                                ScreenData[screenDataIX++] = c;
                                ScreenData[screenDataIX++] = c;
                                ScreenData[screenDataIX++] = c;
                                ScreenData[screenDataIX++] = c;
                                vduval = (byte)(vduval << 1);
                            }
                        }
                        else //(Mode == 4 || Mode == 6 || Mode == 7)
                        {
                            if ((ScreenX & 8) == 0)
                            {
                                vduval = getvidram();
                            }

                            for (int i = 0; i < 4; i++)
                            {
                                Int32 c = ((vduval & 0x80) != 0) ? Palette32[8] : Palette32[0];
                                ScreenData[screenDataIX++] = c;
                                ScreenData[screenDataIX++] = c;
                                vduval = (byte)(vduval << 1);
                            }
                        }

                    }

                    if ((Mode & 4) == 0 || (ScreenX & 8) == 0)
                    {
                        ret = false;
                        CurAddr += 8;
                    }

                }
                else
                {
                    if (render)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            ScreenData[screenDataIX++] = 0;
                        }
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
                }
                else if (ScreenY == CurModeEndY)
                {
                    _isr |= ISR_MASK_DISPEND;
                    UpdateInterrupts();
                }
                if (ScreenY >= ((OddNotEven) ? 312 : 313))
                {
                    ScreenY = 0;
                    OddNotEven = !OddNotEven;
                    screenDataIX = 0;
                    CurCharRowAddr = ScreenStart;
                    if (CurCharRowAddr >= 0x8000)
                        CurCharRowAddr = (ushort)(CurCharRowAddr - CurModeModeLen);
                    CurAddr = CurCharRowAddr;
                    CharScanLine = 0;
                }
                else
                {

                    // next scan line
                    CharScanLine++;
                    if (CharScanLine >= CurModeCharScanLines)
                    {
                        //next char row
                        CharScanLine = 0;

                        CurCharRowAddr += CurModeBytesPerCharRow;
                        if (CurCharRowAddr >= 0x8000)
                            CurCharRowAddr = (ushort)(CurCharRowAddr - CurModeModeLen);

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
                    }
                    else
                    {
                        if (bit == UEFTapeBit.HighTone)
                        {
                            hiToneTicks++;
                            if (hiToneTicks == 60)
                            {
                                _isr |= ISR_MASK_TONE_DETECT;
                                UpdateInterrupts();
                            }
                        }
                        else
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


        byte getvidram()
        {
            ushort tempaddr = CurAddr;
            if (tempaddr >= 0x8000)
                tempaddr -= CurModeModeLen;
            return _ram[tempaddr];
        }
    }
}
