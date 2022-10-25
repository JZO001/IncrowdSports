using System;
using System.Windows.Input;
using System.Windows;

namespace Jzo.IncrowdSports.Client.DroneController.Components
{

    public class NaviDirectionEventBehavior
    {

        public static readonly DependencyProperty DirectionChangedCommandProperty =
                DependencyProperty.RegisterAttached("DirectionChangedCommand", typeof(ICommand), typeof(NaviDirectionEventBehavior), new FrameworkPropertyMetadata(new PropertyChangedCallback(DirectionChangedCommandChanged)));

        private static void DirectionChangedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NaviControl element = (NaviControl)d;
            element.EventDirectionClicked += new EventHandler<ClickedEventArgs>(NaviControl_EventDirectionClicked);
        }

        private static void NaviControl_EventDirectionClicked(object? sender, ClickedEventArgs e)
        {
            NaviControl element = (NaviControl)sender;

            ICommand command = GetDirectionChangedCommand(element);
            command.Execute(e);
        }

        public static void SetDirectionChangedCommand(UIElement element, ICommand value)
        {
            element.SetValue(DirectionChangedCommandProperty, value);
        }

        public static ICommand GetDirectionChangedCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DirectionChangedCommandProperty);
        }

    }

}
