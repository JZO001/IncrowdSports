using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace Jzo.IncrowdSports.Client.DroneController.Components
{

    public class NaviBoolToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool state = (bool)value;
            return state ? Cursors.Hand : Cursors.Arrow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
