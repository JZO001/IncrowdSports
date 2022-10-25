using Jzo.IncrowdSports.Client.DroneController.Data;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Jzo.IncrowdSports.Client.DroneController.Converters
{

    public class ConnectionStatusColorConverter : IValueConverter
    {

        private static readonly Color CONNECTED = Colors.Green;
        private static readonly Color CONNECTING = Colors.Blue;
        private static readonly Color DISCONNECTED = Colors.Red;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ConnectionStateEnum state = (ConnectionStateEnum)value;
            switch (state)
            {
                case ConnectionStateEnum.Connected:
                    return CONNECTED;
                case ConnectionStateEnum.Connecting:
                    return CONNECTING;
            }
            return DISCONNECTED;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
