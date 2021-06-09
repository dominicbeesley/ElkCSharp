using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ElkCSharp.ViewModel
{
    public class ElkModel : ObservableObject
    {
        LEDModel _capsLockLED = new LEDModel() { Name = "Caps Lock" };
        LEDModel _motorLED = new LEDModel() { Name = "Motor" };

        BitmapSource _screenSource = null;
        public BitmapSource ScreenSource
        {
            get { return _screenSource; }
            private set
            {
                _screenSource = value;
                RaisePropertyChangedEvent();
            }
        }

        public LEDModel CapsLockLED { get { return _capsLockLED; } }
        public LEDModel MotorLED { get { return _motorLED; } }

        internal void UpdateScreen(Bitmap screenBmp)
        {
            ScreenSource = screenBmp.ToBitmapSource();
        }
    }
}
