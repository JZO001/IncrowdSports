using System.Windows;
using System.Windows.Input;

namespace Jzo.IncrowdSports.Shared.Codes
{

    /// <summary>For future use, not used right now</summary>
    public class MouseBehaviour
    {

        public static readonly DependencyProperty MouseLeaveCommandProperty =
                DependencyProperty.RegisterAttached("MouseLeaveCommand", typeof(ICommand), typeof(MouseBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseLeaveCommandChanged)));

        public static readonly DependencyProperty MouseEnterCommandProperty =
                DependencyProperty.RegisterAttached("MouseEnterCommand", typeof(ICommand), typeof(MouseBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseEnterCommandChanged)));

        #region MouseLeave handler

        private static void MouseLeaveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element.MouseLeave += new MouseEventHandler(Element_MouseLeave);
        }

        private static void Element_MouseLeave(object sender, MouseEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetMouseLeaveCommand(element);
            command.Execute(e);
        }

        public static void SetMouseLeaveCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseLeaveCommandProperty, value);
        }

        public static ICommand GetMouseLeaveCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseLeaveCommandProperty);
        }

        #endregion

        #region MouseEnter handler

        private static void MouseEnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            element.MouseEnter += new MouseEventHandler(Element_MouseEnter);
        }

        private static void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetMouseEnterCommand(element);
            command.Execute(e);
        }

        public static void SetMouseEnterCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseEnterCommandProperty, value);
        }

        public static ICommand GetMouseEnterCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseEnterCommandProperty);
        }

        #endregion

    }

}
