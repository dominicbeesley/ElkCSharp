using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ElkCSharp.ViewModel
{
    public class HiLoToGreenRed_ValueConverter : IValueConverter
    {
        /// <summary>
        /// Expects red/green to be a ushort 0xRRGG
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int b = (value as Int32?) ?? 0;
                return new SolidColorBrush(Color.FromRgb((byte)(b>>8),(byte)(b & 0xF0),0));
            }
            catch (Exception)
            {
                return new SolidColorBrush(Color.FromRgb(0,0,255));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
