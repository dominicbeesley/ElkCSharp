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

namespace ElkCSharp
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ucLEDSimple : UserControl
    {
        public static readonly DependencyProperty LitProperty =
            DependencyProperty.Register(nameof(Lit), typeof(bool), typeof(ucLEDSimple),
                new FrameworkPropertyMetadata(
                    false,
                    (o, e) => ((ucLEDSimple)o).LitPropertyChanged((bool)e.NewValue)
                    )
                );

        public bool Lit
        {
            get => (bool)GetValue(LitProperty);
            set => SetValue(LitProperty, value);
        }

        private void LitPropertyChanged(bool b)
        {
            ellLED.Opacity = b ? 1.0 : 0.0;
        }

        public ucLEDSimple()
        {
            InitializeComponent();
            LitPropertyChanged(Lit);
        }
    }
}
