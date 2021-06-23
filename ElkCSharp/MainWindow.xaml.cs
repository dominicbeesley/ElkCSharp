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

        private const int N_BUFFERS = 4;
        Bitmap[] bmpCopy;
        volatile UInt32 bmpCopySwitch;

        bool KeysChanged = false;
        byte[] KeyMatrix = new byte[14];
        ElkCSharpSettings.Settings settings;

        /// <summary>
        /// This should be locked when making changes to the emulation from outside the emulatorloop thread
        /// </summary>
        protected object emuLock = new object();

        public MainWindow()
        {
            InitializeComponent();

            AllocConsole();

            try
            {
                var myDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var settingsDir = System.IO.Path.Join(myDir, "Settings");
                settings = ElkCSharpSettings.SettingsFactory.LoadSettings(System.IO.Path.Combine(settingsDir, "settings.xml"));

                bmpCopy = new Bitmap[N_BUFFERS];
                for (int i = 0; i < N_BUFFERS; i++)
                {
                    bmpCopy[i] = new Bitmap(640, 256, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                }

                ColorPalette pal = bmpCopy[0].Palette;
                // this is the _physical_ palette, the logical to physical mapping is done in the rasterizer
                for (int i = 0; i < 256; i++)
                {
                    pal.Entries[i] = System.Drawing.Color.FromArgb(
                        ((i & 1) != 0) ? 255 : 0,
                        ((i & 2) != 0) ? 255 : 0,
                        ((i & 4) != 0) ? 255 : 0
                        );
                }
                bmpCopy.ToList().ForEach(o => o.Palette = pal);


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

                CompositionTarget.Rendering += CompositionTarget_Rendering;
            } catch (Exception ex)
            {
                MessageBox.Show($"An exception occurred\n{ex.ToString()}", $"ERROR:{ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }


        
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {

            var ix = (bmpCopySwitch-1) % N_BUFFERS;
            if (ix < 0) ix += N_BUFFERS;

            var bmp = bmpCopy[ix];
            if (bmp != null)
            {
                lock (bmp)
                {
                    ViewModel.UpdateScreen(bmp);
                }
            }
            Console.Write(ix);
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

                Stopwatch sw = new Stopwatch();
                sw.Start();
                long mymillis = 0;
                long prevmillis = 0;
                while (!cancelToken.IsCancellationRequested)
                {

                    bool fast = ViewModel.GoFast || (Elk.ULA.Motor && ViewModel.GoFastTape);

                    long mil = sw.ElapsedMilliseconds;
                    /*                    long delay = mymillis - mil;
                                        if (delay > 0 & delay < 10000 && !fast)
                                        {
                                            Thread.Sleep((int)(delay));
                                        }
                                        else
                                        {
                                            //we're slow, resync
                                            mymillis = mil;
                                        }
                    */
                    Thread.Sleep(0);
                    long tt = mil - mymillis;
                    if (fast | tt > 20)
                    {


                        lock (Elk)
                        {
                            Elk.DoTicks(40000);
                        }
                        mymillis += 20;

                        if (!fast || mil - prevmillis > 100)
                        {

                            var ix = Interlocked.Increment(ref bmpCopySwitch) % N_BUFFERS;
                            var bmp = bmpCopy[ix];
                            lock (bmp)
                            {
                                var bmpdData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, 640, 256), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                                try
                                {
                                    System.Runtime.InteropServices.Marshal.Copy(Elk.ULA.ScreenData, 0, bmpdData.Scan0, 640 * 256);
                                }
                                finally
                                {
                                    bmp.UnlockBits(bmpdData);
                                }
                            }

                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                ViewModel.CapsLockLED.Lit = Elk.ULA.CapsLock;
                                ViewModel.MotorLED.Lit = Elk.ULA.Motor;
                                ViewModel.TapeToneBiLED.Red = Elk.ULA.LoToneDetect > 8192 ? (byte)255 : (byte)0;
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
                        }
                        else
                        {
                            framectr++;
                        }

                        if (tt > 1000 || tt < -1000)
                        {
                            mymillis = mil;
                        }
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
                ElkCSharpSettings.KeyMap.KeyDef keydef;
                if (FindKeyDef(e.Key, out keydef))
                {
                    KeyMatrix[keydef.Col] |= (byte)(1 << keydef.Row);
                    KeysChanged = true;
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            ElkCSharpSettings.KeyMap.KeyDef keydef;
            if (FindKeyDef(e.Key, out keydef))
            {
                KeyMatrix[keydef.Col] &= (byte)((1 << keydef.Row) ^ 0xF);
                KeysChanged = true;
            }
        }

        private bool FindKeyDef(Key k, out ElkCSharpSettings.KeyMap.KeyDef keydef)
        {
            ElkCSharpSettings.KeyMap.KeyDef? ret = settings.KeyMappings.FirstOrDefault()?.Keys.Where(kd => kd.Key == k).FirstOrDefault();
            if (ret != null)
            {
                keydef = (ElkCSharpSettings.KeyMap.KeyDef)ret;
                return true;
            }
            keydef = ElkCSharpSettings.KeyMap.KeyDef.Empty;
            return false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            emuTaskCancel.Cancel();
            emuThread?.Join(1000);

            bmpCopy?.ToList()?.ForEach(o => o?.Dispose());
            bmpCopy = null;

        }
    }
}
