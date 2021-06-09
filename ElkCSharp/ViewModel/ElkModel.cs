using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ElkHWLib;
using WPFStuff;

namespace ElkCSharp.ViewModel
{
    public class ElkModel : ObservableObject
    {
        public ICommand CmdHardReset { get; }

        Elk _elk;

        LEDModel _capsLockLED = new LEDModel() { Name = "Caps Lock" };
        LEDModel _motorLED = new LEDModel() { Name = "Motor" };

        public ElkModel(Elk elk)
        {
            _elk = elk;

            CmdHardReset = new RelayCommand(
                o =>
                {
                    lock (_elk)
                    {
                        _elk.Reset(true);
                    }
                },
                o => true,
                "Hard Reset",
                Command_Exception
                );
        }

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

        public void Command_Exception(object sender, ExceptionEventArgs args)
        {
            MessageBox.Show(
                args.Exception.ToString(),
                args.Exception.Message,
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );

        }

    }
}
