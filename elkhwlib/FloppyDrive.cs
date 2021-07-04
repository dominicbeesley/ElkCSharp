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

        private int _track;
        public int Track
        {
            get => _track;
            private set
            {
                if (_track != value)
                {
                    _track = value;
                    Track_Changed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler Track_Changed;

        public void Step(bool InNOut)
        {
            if (Sel)
            {
                var t = Track + ((InNOut) ? 1 : -1);
                if (t < 0)
                    t = 0;
                else if (t >= 90)
                    t = 89;
                Track = t;
            }
        }
        public bool Track0 { get => Sel && Track == 0; }


        public bool Active { get => Sel && MotorOn; }

        public void Reset(bool hard) { 
            if (hard)
            {
                Track = new Random().Next(80);
            }        
        }

        public bool _ip;
        public bool IndexPulse
        {
            get => _ip;
            private set
            {
                if (value != _ip)
                {
                    _ip = value;
                    IndexPulse_Changed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler IndexPulse_Changed;

        private static readonly int ROT_MAX = 2000000 / 300; // Tick speed / drive RPM
        private static readonly int IP_WIDTH = 50; // Index pulse width = 25 uS
        private int rotAngle = 0;

        public void Tick()
        {
            if (MotorOn && Sel)
            {
                rotAngle++;
                if (rotAngle >= ROT_MAX)
                    rotAngle = 0;
                if (rotAngle == 0)
                    IndexPulse = true;
                else if (rotAngle == IP_WIDTH)
                    IndexPulse = false;
            }
        }
    }
}
