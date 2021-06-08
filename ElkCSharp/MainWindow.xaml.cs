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

namespace ElkCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int framectr = 0;
        int timerctr = 0;
        int prevframectr = 0;

        bool KeysChanged = false;
        byte[] KeyMatrix = new byte[14];
        Key[][] KeyBoardLayout = new Key[14][]
        {
            new Key [] { Key.Right, Key.End, Key.None, Key.Space},
            new Key [] { Key.Left, Key.Down, Key.Return, Key.Delete},
            new Key [] { Key.Subtract, Key.Up, Key.Oem1, Key.None},
            new Key [] { Key.D0, Key.P, Key.Oem3, Key.None},
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
                Elk elk = new Elk();
                //elk.DebugCycles = true;
                //elk.Debug = true;

                var imgcv = new ImageSourceConverter();


                while (true)
                {
                    elk.DoTicks(40000);
                    Dispatcher.Invoke(() =>
                    {

                        ScreenImg.Source = elk.ULA.ScreenBitmap.ToBitmapSource();

                        framectr++;

                        if (KeysChanged)
                        {
                            elk.UpdateKeys(KeyMatrix);
                            KeysChanged = false;
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
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j <4; j++)
                {
                    if (KeyBoardLayout[i][j] == e.Key)
                    {
                        KeyMatrix[i] |= (byte)(1 << j);
                        KeysChanged = true;
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
