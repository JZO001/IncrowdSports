using Jzo.IncrowdSports.Client.DroneController.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Jzo.IncrowdSports.Client.DroneController.Converters
{

    public class ConnectButtonEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ConnectionStateEnum state = (ConnectionStateEnum)value;
            return state != ConnectionStateEnum.Connecting;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
