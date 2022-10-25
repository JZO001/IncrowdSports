using System;
using System.Globalization;
using System.Windows.Data;

namespace Jzo.IncrowdSports.Client.DroneController.Converters
{

    public class StringIntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }

}
