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
using ElkCSharpSettings;
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

        [DllImport("kernel32.dll")]
        static extern void RtlMoveMemory(IntPtr dest, IntPtr src, uint len);

        protected Elk Elk { get; init; }
        protected ElkModel ViewModel { get; init; }

        int framectr = 0;
        int prevframectr = 0;

        Thread emuThread;
        CancellationTokenSource emuTaskCancel = new CancellationTokenSource();

        private const int N_BUFFERS = 4;
        Int32[][] bmpCopy;
        volatile UInt32 bmpCopySwitch;
        Int32[] palette;

        bool KeysChanged = false;
        byte[] KeyMatrix = new byte[14];
        IList<KeyDef> curPressedKeys = new List<KeyDef>();
        Settings settings;

        /// <summary>
        /// This should be locked when making changes to the emulation from outside the emulatorloop thread
        /// </summary>
        protected object emuLock = new object();

        public MainWindow()
        {
            InitializeComponent();


            try
            {

                var myDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var settingsDir = System.IO.Path.Join(myDir, "Settings");
                settings = SettingsFactory.LoadSettings(System.IO.Path.Combine(settingsDir, "settings.xml"));

                bmpCopy = new Int32[N_BUFFERS][];
                for (int i = 0; i < N_BUFFERS; i++)
                {
                    bmpCopy[i] = new Int32[640 * 256];
                }


                Elk = new Elk();
                //elk.DebugCycles = true;
                //elk.Debug = true;                

                settings.MachineDefs.First().RomDefs.ToList().ForEach(rd =>
                {
                    if (!string.IsNullOrEmpty(rd.Load))
                        Elk.LoadRom(rd.Number, System.IO.Path.Combine(myDir, rd.Load));

                    Elk.RomWriteEnable[rd.Number] = rd.WriteEnable;
                });

                ViewModel = new ElkModel(Elk, settings);
                ViewModel.GoFastTape = true;
                
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

                    unsafe
                    {

                        var w = ViewModel.ScreenSource;
                        w.Lock();
                        byte* ptrD =(byte *) w.BackBuffer;
                        try
                        {
                            fixed (Int32* ptrS0 = &bmp[0])
                            {
                                byte* ptrS = (byte *)ptrS0;
                                for (int i = 0; i < 512; i++)
                                {
                                    RtlMoveMemory((IntPtr)ptrD, (IntPtr)ptrS, 640 * 4);
                                    if ((i & 1) != 0)
                                        ptrS += 640*4;
                                    ptrD += w.BackBufferStride;
                                }
                            }


                            w.AddDirtyRect(new Int32Rect(0, 0, 640, 512));
                        }
                        finally
                        {
                            w.Unlock();
                        }
                    }
                }
            }

            ViewModel.CapsLockLED.Lit = Elk.ULA.CapsLock;
            ViewModel.MotorLED.Lit = Elk.ULA.Motor;
            ViewModel.TapeToneBiLED.Red = Elk.ULA.LoToneDetect > 8192 ? (byte)255 : (byte)0;
            ViewModel.TapeToneBiLED.Green = Elk.ULA.HiToneDetect > 8192 ? (byte)255 : (byte)0;

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
                    bool pause = !ViewModel.Active;

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
                    Thread.Sleep((fast)?0:1);
                    long tt = mil - mymillis;
                    if (!pause && (fast | tt > 20))
                    {

                        bool render = !fast || mil - prevmillis > 20;

                        lock(KeyMatrix)
                        {
                            if (KeysChanged)
                            {
                                Elk.UpdateKeys(KeyMatrix);
                                KeysChanged = false;
                            }
                        }

                        lock (Elk)
                        {
                            Elk.DoTicks(40000, render);
                        }
                        mymillis += 20;

                        if (render)
                        {

                            var ix = Interlocked.Increment(ref bmpCopySwitch) % N_BUFFERS;
                            lock (bmpCopy[ix])
                            {
                                Array.Copy(Elk.ULA.ScreenData, bmpCopy[ix], 640 * 256);
                            }
                            prevmillis = mil;
                        }

                        framectr++;

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
                if (e.IsRepeat)
                    return;

                var curmap = ViewModel.Settings.KeyMappings.Where(o => o.Current).FirstOrDefault() ?? settings.KeyMappings.First();

                var sel = curmap.Keys.Where(
                    o =>
                    o.WindowsKey == e.Key && 
                    (o.Shift == TriState.Any || o.Shift == (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift)?TriState.True:TriState.False)) &&
                    (o.Ctrl == TriState.Any || o.Ctrl == (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) ? TriState.True : TriState.False))
                    );

                curPressedKeys = curPressedKeys.Concat(sel).Distinct().ToList();

                UpdateKeyMatrix();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            var curmap = ViewModel.Settings.KeyMappings.Where(o => o.Current).FirstOrDefault() ?? settings.KeyMappings.First();

            curPressedKeys = curPressedKeys.Where(o =>
                o.WindowsKey != e.Key &&
               (o.Shift == TriState.Any || o.Shift == (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift) ? TriState.True : TriState.False)) &&
               (o.Ctrl == TriState.Any || o.Ctrl == (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) ? TriState.True : TriState.False))
            ).ToList();
            UpdateKeyMatrix();

        }

        private void UpdateKeyMatrix()
        {
            lock (KeyMatrix)
            {
                for (int i = 0; i < KeyMatrix.Length; i++)
                {
                    KeyMatrix[i] = 0;
                }

                foreach (var km in curPressedKeys.SelectMany(o => o.KeyMatrices).Where(o => o.On))
                    KeyMatrix[km.Col] |= (byte)(1 << km.Row);
                foreach (var km in curPressedKeys.SelectMany(o => o.KeyMatrices).Where(o => !o.On))
                    KeyMatrix[km.Col] &= (byte)((1 << km.Row) ^ 0xFF);
                KeysChanged = true;
            }
        }
       
        private void Window_Closed(object sender, EventArgs e)
        {
            emuTaskCancel.Cancel();
            emuThread?.Join(1000);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            ViewModel.WindowPause = true;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ViewModel.WindowPause = false;
        }
    }
}
