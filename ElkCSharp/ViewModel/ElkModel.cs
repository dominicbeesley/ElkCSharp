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
using Microsoft.Win32;
using WPFStuff;
using ElkCSharpSettings;
using System.Collections.ObjectModel;

namespace ElkCSharp.ViewModel
{
    public class ElkModel : ObservableObject
    {
        public ICommand CmdHardReset { get; }
        public ICommand CmdTapeLoad { get; }

        public ICommand CmdTapeRewind { get; }
        public ICommand CmdTapeEject { get; }

        Elk _elk;

        LEDModel _capsLockLED = new LEDModel() { Name = "Caps Lock" };
        LEDModel _motorLED = new LEDModel() { Name = "Motor" };
        BiLEDModel _tapeToneBiLED = new BiLEDModel() { Name = "Tape Tone" };

        public Settings Settings { get; init; }


        public ObservableCollection<System.Windows.Controls.MenuItem> KeyboardMenu
        {
            get; init;
        }


        public ElkModel(Elk elk, Settings settings)
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

            CmdTapeLoad = new RelayCommand(
                o =>
                {
                    var dlg = new OpenFileDialog();
                    dlg.Filter = "UEF Files|*.uef";
                    if (dlg.ShowDialog() ?? false)
                    {
                        lock (_elk)
                        {
                            try
                            {
                                _elk.ULA.UEF?.Dispose();
                                _elk.ULA.UEF = new UEFLib.UEFTapeStreamer(dlg.FileName, true);
                            } catch (Exception)
                            {
                                _elk.ULA.UEF = null;
                                throw;
                            }

                        }
                    }

                },
                o => true,
                "Load Tape",
                Command_Exception
            );

            CmdTapeRewind = new RelayCommand(
                o =>
                {
                    lock (_elk)
                    {
                        _elk.ULA?.UEF?.Rewind();
                    }
                },
                o => _elk.ULA.UEF != null,
                "Rewind Tape",
                Command_Exception
                ) ;

            CmdTapeEject = new RelayCommand(
                o =>
                {
                    lock (_elk)
                    {
                        var old = _elk.ULA.UEF;
                        _elk.ULA.UEF = null;
                        old.Dispose();
                    }
                },
                o => _elk.ULA.UEF != null,
                "Eject Tape",
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
        public BiLEDModel TapeToneBiLED { get { return _tapeToneBiLED; } }

        private bool _goFast;
        /// <summary>
        /// Run emulation flat out
        /// </summary>
        public bool GoFast { 
            get { return _goFast; } 
            set
            {
                _goFast = value;
                RaisePropertyChangedEvent();
            }
        }

        private bool _goFastTape;
        public bool GoFastTape
        {
            get { return _goFastTape; }
            set
            {
                if (_goFastTape != value)
                {
                    _goFastTape = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private bool _userPause;
        /// <summary>
        /// User request pause using menu
        /// </summary>
        public bool UserPause
        {
            get { return _userPause; }
            set
            {
                if (_userPause != value)
                {
                    _userPause = value;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(Active));
                }
            }
        }

        private bool _windowPause;
        /// <summary>
        /// Window deactiveated causes pause
        /// </summary>
        public bool WindowPause
        {
            get { return _windowPause; }
            set
            {
                if (_windowPause != value)
                {
                    _windowPause = value;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(Active));
                }
            }
        }

        private bool _pauseOnFocus;
        /// <summary>
        /// Window deactiveated causes pause
        /// </summary>
        public bool PauseOnFocus
        {
            get { return _pauseOnFocus; }
            set
            {
                if (_pauseOnFocus != value)
                {
                    _pauseOnFocus = value;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(Active));
                }
            }
        }

        /// <summary>
        /// Whether the emulation should be active there are several reasons why it might be paused:
        ///  - deactivated window
        ///  - user paused
        ///  - debug 
        ///  - etc
        /// </summary>
        public bool Active
        {
            get { return !UserPause && !(WindowPause & PauseOnFocus); }
        }

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
