using Jzo.IncrowdSports.Client.DroneController.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Jzo.IncrowdSports.Client.DroneController.Converters
{
    internal class ConnectButtonStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ConnectionStateEnum state = (ConnectionStateEnum)value;
            switch (state)
            {
                case ConnectionStateEnum.Connected:
                    return "Disconnect";
                case ConnectionStateEnum.Connecting:
                    return "In progress...";
            }
            return "Connect";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
