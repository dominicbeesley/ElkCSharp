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
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Utility;
using System.Collections.Concurrent;

namespace ElkCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        protected Elk Elk { get; init; }
        protected ElkModel ViewModel { get; init; }

        int framectr = 0;
        int prevframectr = 0;

        Thread emuThread;
        CancellationTokenSource emuTaskCancel = new CancellationTokenSource();

        Bitmap bmpCopy;

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

        /// <summary>
        /// This should be locked when making changes to the emulation from outside the emulatorloop thread
        /// </summary>
        protected object emuLock = new object();

        public MainWindow()
        {
            InitializeComponent();

            AllocConsole();

            bmpCopy = new Bitmap(640, 256, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            Elk = new Elk();
            //elk.DebugCycles = true;
            //elk.Debug = true;

            ViewModel = new ElkModel(Elk);

            this.DataContext = ViewModel;

            emuThread = new Thread(() => EmulatorLoop(emuTaskCancel.Token));
            emuThread.Priority = ThreadPriority.AboveNormal;
            emuThread.Start();

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

        public void EmulatorLoop(CancellationToken cancelToken)
        {
            try
            {
                ColorPalette pal = bmpCopy.Palette;
                // this is the _physical_ palette, the logical to physical mapping is done in the rasterizer
                for (int i = 0; i < 256; i++)
                {
                    pal.Entries[i] = System.Drawing.Color.FromArgb(
                        ((i & 1) != 0) ? 255 : 0,
                        ((i & 2) != 0) ? 255 : 0,
                        ((i & 4) != 0) ? 255 : 0
                        );
                }
                bmpCopy.Palette = pal;

                Stopwatch sw = new Stopwatch();
                sw.Start();
                long mymillis = 0;
                long prevmillis = 0;
                while (!cancelToken.IsCancellationRequested)
                {

                    bool fast = ViewModel.GoFast || (Elk.ULA.Motor && ViewModel.GoFastTape);

                    long mil = sw.ElapsedMilliseconds;
                    long delay = mymillis - mil;
                    if (delay > 0 & delay < 10000 && !fast)
                    {
                        Thread.Sleep((int)(delay));
                    }
                    else
                    {
                        //we're slow, resync
                        mymillis = mil;
                    }

                    lock (Elk)
                    {
                        Elk.DoTicks(40000);
                    }
                    mymillis += 20;

                    if (!fast || mil - prevmillis > 15)
                    {

                        lock (bmpCopy)
                        {
                            var bmpdData = bmpCopy.LockBits(new System.Drawing.Rectangle(0, 0, 640, 256), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                            try
                            {
                                System.Runtime.InteropServices.Marshal.Copy(Elk.ULA.ScreenData, 0, bmpdData.Scan0, 640 * 256);
                            }
                            finally
                            {
                                bmpCopy.UnlockBits(bmpdData);
                            }
                        }

                        Dispatcher.BeginInvoke(new Action(() =>
                        {

                            if (bmpCopy != null)
                            {
                                lock (bmpCopy)
                                {
                                    ViewModel.UpdateScreen(bmpCopy);
                                }
                            }
                            ViewModel.CapsLockLED.Lit = Elk.ULA.CapsLock;
                            ViewModel.MotorLED.Lit = Elk.ULA.Motor;
                            ViewModel.TapeToneBiLED.Red = Elk.ULA.LoToneDetect>8192?(byte)255:(byte)0;
                            ViewModel.TapeToneBiLED.Green = Elk.ULA.HiToneDetect > 8192 ? (byte)255 : (byte)0;

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

                        }));
                        prevmillis = mil;
                    } else
                    {
                        framectr++;
                    }

                }
            }
            catch (TaskCanceledException)
            {
                return;
            }
            catch (Exception ex)
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
                lock (Elk)
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

        private void Window_Closed(object sender, EventArgs e)
        {
            emuTaskCancel.Cancel();
            emuThread.Join(1000);
            var b = bmpCopy;
            bmpCopy = null;
            b.Dispose();
        }
    }
}
