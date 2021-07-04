using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkHWLib
{
    public class FloppyDrive
    {
        /// <summary>
        /// The drive Motor is on
        /// </summary>
        public bool MotorOn { get; set; }
        /// <summary>
        /// Drive 0 is selected
        /// </summary>
        public bool Sel { get; set; }
        /// <summary>
        /// Side A is selected (else side B)
        /// </summary>
        public bool SideAnB { get; set; }

        public int Track { get; }
        public void Step(bool InNOut)
        {
            var t = Track + ((InNOut) ? -1 : 1);
            if (t < 0)
                t = 0;
            else if (t >= 90)
                t = 89;
        }
        public bool Track0 { get => Sel && Track == 0; }

        public bool Active { get => Sel; }
    }
}
