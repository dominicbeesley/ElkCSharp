using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Utility;

namespace ElkCSharp.ViewModel
{
    public class FloppyDriveModel : ObservableObject
    {
        private ElkHWLib.FloppyDrive _drive;

        private bool _motorOn;
        public bool MotorOn
        {
            get => _motorOn;
            set => Set(ref _motorOn, value);
        }


        public int DriveNumber
        {
            get;
        }
        private int _trackNumber;
        public int TrackNumber
        {
            get => _trackNumber;
            set => Set(ref _trackNumber, value);
        }

        public FloppyDriveModel(int number, ElkHWLib.FloppyDrive drive)
        {
            DriveNumber = number;
            _drive = drive;

            if (_drive != null)
            {
                _drive.MotorOn_Changed += DriveState_Changed;
                _drive.Sel_Changed += DriveState_Changed;
                _drive.Track_Changed += DriveState_Changed;
                Update();
            }
        }

        protected void DriveState_Changed(object sender, EventArgs e)
        {
            Update();
        }

        protected void Update()
        {
            MotorOn = _drive.MotorOn && _drive.Sel;
            TrackNumber = _drive.Track;
        }
    }
}
