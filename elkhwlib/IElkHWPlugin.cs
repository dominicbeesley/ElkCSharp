using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkHWLib
{
    public interface IElkHWPlugin
    {
        /// <summary>
        /// called by the main loop once every 2MHz tick
        /// </summary>
        public void Tick();

        /// <summary>
        /// Called for all memory reads that match mask/base
        /// </summary>
        /// <param name="addr">The address to read</param>
        /// <param name="dat">The returned data byte</param>
        /// <param name="peek">If true, side effects should be skipped i.e. a read by the debugger etc</param>
        /// <returns>true if this hardware handled this read</returns>
        public bool Read(ushort addr, ref byte dat, bool peek = false);

        /// <summary>
        /// Called for all memory writes that match mask/base
        /// </summary>
        /// <param name="addr">The address to read</param>
        /// <param name="dat">The returned data byte</param>
        /// <returns>true if this hardware handled this write</returns>
        public bool Write(ushort addr, byte dat);

        public bool IRQ { get; }
        public bool NMI { get; }

        /// <summary>
        /// The Read/Write interface methods will be called for addresses where
        /// (addr & AddrMask) == AddrBase
        /// </summary>
        public ushort AddrMask { get; }
        /// <summary>
        /// The Read/Write interface methods will be called for addresses where
        /// (addr & AddrMask) == AddrBase
        /// </summary>
        public ushort AddrBase { get; }

        public void Reset(bool hard);

    }
}
