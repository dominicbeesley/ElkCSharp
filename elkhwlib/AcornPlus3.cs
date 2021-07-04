using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkHWLib
{
    public class AcornPlus3 : IElkHWPlugin
    {
        

        private Elk _elk;
        private Wd1770 _wd1770;

        public bool IRQ => false;

        public bool NMI
        {
            get;
            protected set;
        }

        public ushort AddrMask => 0xFFF8;

        public ushort AddrBase => 0xFCC0;

        public bool MotorOn { get; private set; }

        public bool Sel0 { get; private set; }

        public bool Sel1 { get; private set; }

        public bool SideAnB { get; private set; }

        public bool Read(ushort addr, ref byte dat, bool peek = false)
        {

            bool ret = false;

            if ((addr & 4) != 0)
            {
                _wd1770.Read(addr, out dat, peek);
                ret = true;
            }    

            return ret;
        }

        public void Tick()
        {
            _wd1770.Tick();
        }

        public bool Write(ushort addr, byte dat)
        {
            if ((addr & 4) == 0)
            {
                _elk.FloppyDrive0.SideAnB = (dat & 4) != 0;
                _elk.FloppyDrive1.SideAnB = (dat & 4) != 0;
                _elk.FloppyDrive0.Sel = (dat & 1) != 0;
                _elk.FloppyDrive1.Sel = (dat & 2) != 0;
                _wd1770.DDEN = (dat & 0x08) == 0;
                _wd1770.MR = (dat & 0x20) == 0;
            } else {
                _wd1770.Write(addr, dat);
            }
            return true;
        }

        public AcornPlus3(Elk emulatorRoot)
        {
            _elk = emulatorRoot;
            _wd1770 = new Wd1770();
        }
    }
}
