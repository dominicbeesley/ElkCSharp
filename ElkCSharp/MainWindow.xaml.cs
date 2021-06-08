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

                var imgcv = new ImageSourceConverter();


                while (true)
                {
                    elk.DoTicks(40000);
                    Dispatcher.Invoke(() =>
                    {

                        ScreenImg.Source = elk.ULA.ScreenBitmap.ToBitmapSource();

                        framectr++;

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
    }
}
