using cpulib_65xx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkHWLib
{
    public class Elk : ISYSCpu
    {
        private byte[] _mos;
        private byte[] _rom_basic;
        private byte[] _ram;

        public ULA ULA { get; }
        public m6502_device CPU { get; }

        public Elk()
        {
            ULA = new ULA();
            CPU = new m6502_device(this);

            CPU.start();
            CPU.reset();


            _mos = LoadRom(0x4000, "d:\\downloads\\ELK100");
            _rom_basic = LoadRom(0x4000, "d:\\downloads\\B_BASIC200");
            _ram = new byte[0x8000];
        }

        public void Read(ushort addr, out byte dat, bool peek = false)
        {
            if ((addr & 0xC000) == 0xC000)
            {
                if (addr > 0xFC00 && addr < 0xFEFF)
                {
                    if (addr > 0xFE00 & addr < 0xFE0F)
                    {
                        dat = ULA.ReadReg(addr & 0xF);
                    } else
                    {
                        // read of "empty" hardware returns previous DAT!?
                        dat = CPU.DAT;                  
                    }
                }

                dat = _mos[addr & 0x3FFF];
            } 
            else if ((addr & 0x8000) != 0)
            {
                dat = _rom_basic[addr & 0x3FFF];
            } 
            else
            {
                dat = _ram[addr & 0x3FFF];   
            }
        }

        public void DoTicks(int nTicks)
        {
            for (int i = 0; i < nTicks; i++)
            {
                if (ULA.tick(CPU.ADDR))
                    CPU.tick();
            }
        }

        public void Write(ushort addr, byte dat)
        {
            if ((addr & 0x8000) == 0)
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
    }
}
