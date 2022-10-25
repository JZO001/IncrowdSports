using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Jzo.IncrowdSports.Client.DroneController.Components
{

    public class NaviBoolToColorConverter : IValueConverter
    {

        private static readonly Color SLB_ACTIVE = Colors.Black;
        private static readonly Color SLB_INACTIVE = Colors.Gray;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool state = (bool)value;
            return state ? SLB_ACTIVE : SLB_INACTIVE;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
