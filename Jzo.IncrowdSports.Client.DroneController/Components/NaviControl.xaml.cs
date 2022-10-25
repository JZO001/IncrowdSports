using Jzo.IncrowdSports.Shared.Messages;
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

namespace Jzo.IncrowdSports.Client.DroneController.Components
{

    /// <summary>
    /// Interaction logic for NaviControl.xaml
    /// </summary>
    public partial class NaviControl : UserControl
    {

        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.Register("Enabled", 
                typeof(bool), 
                typeof(NaviControl), 
                new UIPropertyMetadata(false, new PropertyChangedCallback(EnabledChangedHandler)));

        private INaviControlViewModel? _viewModel;

        public event EventHandler<ClickedEventArgs> EventDirectionClicked;

        public NaviControl()
        {
            InitializeComponent();
        }

        #region Enabled handler

        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set 
            { 
                SetValue(EnabledProperty, value);
                //((INaviControlViewModel)this.DataContext).IsEnabled = value;
            }
        }

        private static void EnabledChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NaviControl ctrl = (NaviControl)d;
            ((INaviControlViewModel)ctrl.DataContext).IsEnabled = (bool)e.NewValue;
        }

        #endregion

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _viewModel = (INaviControlViewModel)e.NewValue;
        }

        private void RaiseEventDirectionClicked(MoveDirection2DEnum direction)
        {
            if (Enabled) EventDirectionClicked?.Invoke(this, new ClickedEventArgs(direction));
        }

        private void MoveUp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseEventDirectionClicked(MoveDirection2DEnum.Forward);
        }

        private void MoveLeft_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseEventDirectionClicked(MoveDirection2DEnum.Left);
        }

        private void MoveRight_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseEventDirectionClicked(MoveDirection2DEnum.Right);
        }

        private void MoveDown_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseEventDirectionClicked(MoveDirection2DEnum.Backward);
        }
    }

}
