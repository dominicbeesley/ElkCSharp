using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkCSharp.ViewModel
{
    public class BiLEDModel : ObservableObject
    {
        byte _red = 0;
        byte _green = 0;
        public string Name { get; init; }

        public byte Red
        {
            get { return _red; }
            set
            {
                if (value != _red)
                {
                    _red = value;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(BiLEDValue));
                }
            }
        }
        public byte Green
        {
            get { return _green; }
            set
            {
                if (value != _green)
                {
                    _green = value;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(BiLEDValue));
                }
            }
        }

        public int BiLEDValue
        {
            get { return (int)((Red << 8) | Green); }
        }
    }
}
