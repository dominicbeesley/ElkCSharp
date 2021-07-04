using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace ElkCSharp.ViewModel
{
    public class FloppyDriveModel : ObservableObject
    {
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

        public FloppyDriveModel(int number)
        {
            DriveNumber = number;
        }

    }
}
