using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace KeyNames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Keys { get; init; }
        public ObservableCollection<string> Keys2 { get; init; }

        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;

            Keys = new ObservableCollection<string>();
            Keys2 = new ObservableCollection<string>();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.IsRepeat)
                Keys.Add(e.Key.ToString());
            UpdateKeys2();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Keys.Select((a, i) => (str: a, ix: i)).Where(o => o.str == e.Key.ToString()).Select(o => o.str).ToList().ForEach(
                str => Keys.Remove(str)
                );
            UpdateKeys2();
        }

        protected void UpdateKeys2()
        {
            Keys2.Clear();
            foreach (var k in ((Key[])Enum.GetValues(typeof(Key))).Where(kk => kk != Key.None))
            {
                if (Keyboard.IsKeyDown(k))
                    Keys2.Add(k.ToString());
            }
        }
    }
}
