using cpulib_65xx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkHWLib
{
    public class Elk : ISYSCpu, IDisposable
    {
        public static readonly byte ROMNO_BASIC = 10;
        public static readonly byte ROMNO_KEYBOARD = 12;
        public static readonly byte ROMNO_MOS = 16;

        private byte[] KeyMatrix = new byte[14];

        public byte[] Ram { get; init; }
        public byte[][] Roms { get; init; }
        public bool[] RomWriteEnable { get; init; }

        public byte [] RAM { get { return Ram; } }
        public ULA ULA { get; }
        public m6502_device CPU { get; }
        public bool DebugCycles { get; set; }
        public bool Debug
        {
            get
            {
                return _debugStream != null;
            }
            set
            {
                if (value == true && _debugStream == null)
                    _debugStream = File.CreateText(@"d:\temp\elksharp.txt");
                else
                {
                    _debugStream.Dispose();
                    _debugStream = null;
                }
            }
        }

        TextWriter _debugStream = null;
        private bool disposedValue;

        IElkHWPlugin[] hardwarePlugins;

        public FloppyDrive FloppyDrive0 { get; init; }
        public FloppyDrive FloppyDrive1 { get; init; }

        public Elk()
        {
            ULA = new ULA();
            CPU = new m6502_device(this);


            ULA.IRQChange += (o, e) => {
                CPU.execute_set_input(cpu_device.cpu_65xx_inputlines.INPUT_LINE_IRQ0, ULA.IRQ ? cpu_device.cpu_65xx_inputstate.ASSERT_LINE : cpu_device.cpu_65xx_inputstate.CLEAR_LINE);
            };


            Ram = new byte[0x8000];
            Roms = new byte[17][];
            RomWriteEnable = new bool[17];
            for (int i = 0; i<=16; i++)
            {
                Roms[i] = new byte[0x4000];
            }

            FloppyDrive0 = new FloppyDrive();
            FloppyDrive1 = new FloppyDrive();

            hardwarePlugins = new IElkHWPlugin[]
            {
                    new AcornPlus3(this)
            };

            Reset(true);
        }

        public void Reset(bool hard) { 
            
            CPU.start();
            CPU.reset();
            ULA.Reset(hard);
            foreach (var h in hardwarePlugins)
            {
                h.Reset(hard);
            }
            FloppyDrive0.Reset(hard);
            FloppyDrive1.Reset(hard);
        }

        public bool Read(ushort addr, ref byte dat, bool peek = false)
        {
            if ((addr & 0xC000) == 0xC000)
            {
                if (addr >= 0xFC00 && addr < 0xFEFF)
                {
                    bool ret = false;
                    if ((addr & 0xFF00) == 0xFE00)
                    {
                        ret = ULA.ReadReg(addr, ref dat);
                    }
                    else
                    {
                        foreach (var hw in hardwarePlugins)
                        {
                            if ((addr & hw.AddrMask) == hw.AddrBase)
                                if (hw.Read(addr, ref dat, peek))
                                {
                                    ret = true;
                                    break;
                                }
                        }
                    }

                    //force to FF if no read in HW area not sure if this is right or not
                    //but is required for DFS/ADFS see https://stardot.org.uk/forums/viewtopic.php?f=3&t=22031&hilit=electron+adfs+dominicbeesley
                    if (!ret)
                        dat = 0xFF;

                    return ret;
                }
                else
                {
                    dat = Roms[ROMNO_MOS][addr & 0x3FFF];
                    return true;
                }
            } 
            else if ((addr & 0x8000) != 0)
            {
                if (!ULA.ROM_External)
                {
                    if ((ULA.ROM_IntBank & 2) != 0)
                    {
                        dat = Roms[ROMNO_BASIC][addr & 0x3FFF];
                        return true;
                    }
                    else
                    {
                        //keyboard read
                        ushort m = 1;
                        byte ret = 0;
                        for (int i = 0; i < 14; i++)
                        {
                            if ((addr & m) == 0)
                            {
                                ret |= KeyMatrix[i];
                            }

                            m <<= 1;
                        }
                        dat = ret;
                        return true;
                    }
                } else
                {

                    dat = Roms[ULA.ROM_ExtBank][addr & 0x3FFF];
                    return true;
                }
            } 
            else
            {
                dat = Ram[addr & 0x7FFF];
                return true;
            }
        }

        bool prevCPU1MHz = false;

        /// <summary>
        /// Run the emulation
        /// </summary>
        /// <param name="nTicks">Number of 2MHz ticks to perform</param>
        /// <param name="render">If true then vide/sound are rendered</param>
        public void DoTicks(int nTicks, bool render)
        {
            for (int i = 0; i < nTicks; i++)
            {
                bool cpuEN = ULA.Tick(render);
                bool ram = (CPU.ADDR & 0x8000) == 0;
                bool go = false;
                
                if (ram & prevCPU1MHz)
                {
                    if (cpuEN)
                    {
                        go = true;
                    }
                } 
                else if (ram)
                {
                    prevCPU1MHz = true;
                } 
                else if (!ram)
                {
                    prevCPU1MHz = false;
                    go = true;
                }


                if (go) {
                    CPU.tick();
                    if (_debugStream != null)
                    {
                        if (DebugCycles || CPU.Sync)
                        {
                            _debugStream.WriteLine($"PC={CPU.PC:X4},A={CPU.A:X2},X={CPU.X:X2},Y={CPU.Y:X2},P={CPU.P:X2}({CPU.FlagsToString(CPU.P)}),ADDR={CPU.ADDR:X4},DAT={CPU.DAT:X2}");
                        }
                        if (CPU.Sync)
                        {
                            _debugStream.WriteLine(CPU.Disassemble(CPU.PC));
                        }
                    }
                }

                foreach (var hw in hardwarePlugins)
                    hw.Tick();
            }
        }

        public void Write(ushort addr, byte dat)
        {
            if ((addr & 0xFF00) == 0xFE00)
            {
                ULA.WriteReg(addr, dat);
            }
            else if ((addr & 0x8000) == 0)
            {
                Ram[addr] = dat;
                ULA.RamWrite(addr, dat);
            }
            else if ((addr & 0xFF00) == 0xFC00 || (addr & 0xFF00) == 0xFD00)
            {
                foreach (var hw in hardwarePlugins)
                {
                    if ((addr & hw.AddrMask) == hw.AddrBase)
                        if (hw.Write(addr, dat))
                            return;
                }
            }
        }

        public void LoadRom(int romno, string filename)
        {

            const int len = 0x4000;

            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                int i = 0;
                while (i < len)
                {
                    int ll = len - i;
                    int s = fs.Read(Roms[romno], i, ll);
                    if (s < 0)
                        break;
                    else
                        i += s;
                }
            }
        }

        public void UpdateKeys(byte[] matrix)
        {
            matrix.CopyTo(KeyMatrix, 0);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_debugStream != null)
                    {                        
                        _debugStream.Dispose();
                        _debugStream = null;
                    }

                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Elk()
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
