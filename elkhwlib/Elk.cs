﻿using cpulib_65xx;
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
        private byte[] KeyMatrix = new byte[14];

        private byte[] _mos;
        private byte[] _rom_basic;
        private byte[] _ram;

        public byte [] RAM { get { return _ram; } }
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

        public Elk()
        {
            ULA = new ULA();
            CPU = new m6502_device(this);

            Reset(true);

            ULA.IRQChange += (o, e) => {
                CPU.execute_set_input(cpu_device.cpu_65xx_inputlines.INPUT_LINE_IRQ0, ULA.IRQ ? cpu_device.cpu_65xx_inputstate.ASSERT_LINE : cpu_device.cpu_65xx_inputstate.CLEAR_LINE);
            };


            _mos = LoadRom(0x4000, "d:\\downloads\\ELK100");
            _rom_basic = LoadRom(0x4000, "d:\\downloads\\B_BASIC200");
            _ram = new byte[0x8000];

        }

        public void Reset(bool hard) { 
            
            CPU.start();
            CPU.reset();
            ULA.Reset(hard);

        }

        public void Read(ushort addr, out byte dat, bool peek = false)
        {
            if ((addr & 0xC000) == 0xC000)
            {
                if (addr >= 0xFC00 && addr < 0xFEFF)
                {
                    if ((addr & 0xFF00) == 0xFE00)
                    {
                        if (!ULA.ReadReg(addr, out dat))
                        {
                            dat = CPU.DAT;
                        }
                    } else
                    {
                        // read of "empty" hardware returns previous DAT!?
                        dat = CPU.DAT;                  
                    }
                } else 
                    dat = _mos[addr & 0x3FFF];
            } 
            else if ((addr & 0x8000) != 0)
            {
                if (!ULA.ROM_External)
                {
                    if ((ULA.ROM_IntBank & 2) != 0)
                        dat = _rom_basic[addr & 0x3FFF];
                    else
                    {
                        //keyboard read
                        ushort m = 1;
                        byte ret = 0;
                        for (int i = 0; i <14; i++)
                        {
                            if ((addr & m) == 0)
                            {
                                ret |= KeyMatrix[i];
                            }

                            m <<= 1;
                        }
                        dat = ret;
                    }
                } else
                {
                    dat = CPU.DAT;
                }
            } 
            else
            {
                dat = _ram[addr & 0x7FFF];   
            }
        }

        bool prevCPU1MHz = false;

        public void DoTicks(int nTicks)
        {
            for (int i = 0; i < nTicks; i++)
            {
                bool cpuEN = ULA.Tick();
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
                _ram[addr] = dat;
                ULA.RamWrite(addr, dat);
            }
        }

        public byte [] LoadRom(int len, string filename)
        {
            byte[] ret = new byte[len];

            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                int i = 0;
                while (i < len)
                {
                    int ll = len - i;
                    int s = fs.Read(ret, i, ll);
                    if (s < 0)
                        break;
                    else
                        i += s;
                }
            }

            return ret;
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
