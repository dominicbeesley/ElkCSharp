using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace ElkCSharp.ViewModel
{
    public class BiLEDModel : ObservableObject
    {
        byte _red = 0;
        byte _green = 0;
        public string Name { get; init; }

        public byte Red
        {
            get => _red;
            set {
                if (Set(ref _red, value)) { 
                    RaisePropertyChangedEvent(nameof(BiLEDValue));
                }
            }
        }
        public byte Green
        {
            get => _green;
            set
            {
                if (Set( ref _green, value))
                {
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
