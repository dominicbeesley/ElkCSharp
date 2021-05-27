using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpulib_65xx
{

    /// <summary>
    /// An interface for the cpu to read/write from the "motherboard"
    /// </summary>
    public interface ISYSCpu
    {
        bool Read(ushort addr, ref byte dat);
        bool Write(ushort addr, byte dat);
    }
}
