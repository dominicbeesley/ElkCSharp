using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ElkHWLib;
using cpulib_65xx;
using System.IO;
using ElkCSharp.ViewModel;


namespace ElkCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        protected Elk Elk { get; init; }
        protected ElkModel ViewModel { get; init; }

        int framectr = 0;
        int prevframectr = 0;

        bool KeysChanged = false;
        byte[] KeyMatrix = new byte[14];
        Key[][] KeyBoardLayout = new Key[14][]
        {
            new Key [] { Key.Right, Key.End, Key.None, Key.Space},
            new Key [] { Key.Left, Key.Down, Key.Return, Key.Delete},
            new Key [] { Key.Subtract, Key.Up, Key.Oem1, Key.None},
            new Key [] { Key.D0, Key.P, Key.Oem3, Key.Oem2},
            new Key [] { Key.D9, Key.O, Key.L, Key.OemPeriod},
            new Key [] { Key.D8, Key.I, Key.K, Key.OemComma},
            new Key [] { Key.D7, Key.U, Key.J, Key.M},
            new Key [] { Key.D6, Key.Y, Key.H, Key.N},
            new Key [] { Key.D5, Key.T, Key.G, Key.B},
            new Key [] { Key.D4, Key.R, Key.F, Key.V},
            new Key [] { Key.D3, Key.E, Key.D, Key.C},
            new Key [] { Key.D2, Key.W, Key.S, Key.X},
            new Key [] { Key.D1, Key.Q, Key.A, Key.Z},
            new Key [] { Key.Escape, Key.CapsLock, Key.LeftCtrl, Key.LeftShift}
        };

        public MainWindow()
        {
            InitializeComponent();

            Elk = new Elk();
            //elk.DebugCycles = true;
            //elk.Debug = true;

            ViewModel = new ElkModel(Elk);

            this.DataContext = ViewModel;


            Task.Run(() => EmulatorLoop());

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            float fps = framectr - prevframectr;
            Title = $"{framectr} : {fps}";
            prevframectr = framectr;
        }

        public void EmulatorLoop()
        {
            try
            {

                var x = new UEFLib.UEFChunkReader(@"D:\downloads\Firetrack_E.gz.uef", true);

                
                while (true)
                {
                    lock (Elk)
                    {
                        Elk.DoTicks(40000);
                    }
                    Dispatcher.Invoke(() =>
                    {

                        ViewModel.UpdateScreen(Elk.ULA.ScreenBitmap);
                        ViewModel.CapsLockLED.Lit = Elk.ULA.CapsLock;
                        ViewModel.MotorLED.Lit = Elk.ULA.Motor;

                        framectr++;

                        if (KeysChanged)
                        {
                            Elk.UpdateKeys(KeyMatrix);
                            KeysChanged = false;
                        }

                        if (framectr == 100)
                        {
                            //TEST:
                            byte[] testprog = File.ReadAllBytes(@"d:\downloads\HOGELKTI");
                            testprog.CopyTo(Elk.RAM, 0xE00);
                            Elk.ULA.SyncRAM(Elk.RAM);
                        }

                    });
                }
            } catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(ex.ToString());
                });
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                lock(Elk)
                {
                    Elk.Reset(false);
                }
            }
            else
            {

                for (int i = 0; i < 14; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (KeyBoardLayout[i][j] == e.Key)
                        {
                            KeyMatrix[i] |= (byte)(1 << j);
                            KeysChanged = true;
                        }

                    }
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (KeyBoardLayout[i][j] == e.Key)
                    {
                        KeyMatrix[i] &= (byte)((1 << j) ^ 0xF);
                        KeysChanged = true;
                    }
                }
            }

        }
    }
}
