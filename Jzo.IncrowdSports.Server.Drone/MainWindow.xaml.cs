using Jzo.IncrowdSports.Server.Drone.ViewModels;
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

namespace Jzo.IncrowdSports.Server.Drone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool _firstSeen = true;
        private const double DRONE_WIDTH = 148d;
        private const double DRONE_HEIGHT = 56d;
        private double _canvasWidth = 0;
        private double _canvasHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IMainWindowViewModel viewModel = (IMainWindowViewModel)e.NewValue;
            viewModel.EventMessageMove += ViewModel_EventMessageMove;
        }

        private void ViewModel_EventMessageMove(object? sender, Data.MoveEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Console.WriteLine($"Direction: {e.MessageMove.Direction}, SpeedStep: {e.MessageMove.SpeedStep}");
                switch (e.MessageMove.Direction)
                {
                    case MoveDirection2DEnum.Backward:
                        {
                            double vertPos = Canvas.GetTop(drone);
                            vertPos += e.MessageMove.SpeedStep;
                            if (vertPos + DRONE_HEIGHT > _canvasHeight) vertPos = _canvasHeight - DRONE_HEIGHT;
                            Canvas.SetTop(drone, vertPos);
                        }
                        break;

                    case MoveDirection2DEnum.Forward:
                        {
                            double vertPos = Canvas.GetTop(drone);
                            vertPos -= e.MessageMove.SpeedStep;
                            if (vertPos < 0) vertPos = 0;
                            Canvas.SetTop(drone, vertPos);
                        }
                        break;

                    case MoveDirection2DEnum.Left:
                        {
                            double horPos = Canvas.GetLeft(drone);
                            horPos -= e.MessageMove.SpeedStep;
                            if (horPos < 0) horPos = 0;
                            Canvas.SetLeft(drone, horPos);
                        }
                        break;

                    case MoveDirection2DEnum.Right:
                        {
                            double horPos = Canvas.GetLeft(drone);
                            horPos += e.MessageMove.SpeedStep;
                            if (horPos + DRONE_WIDTH > _canvasWidth) horPos = _canvasWidth - DRONE_WIDTH;
                            Canvas.SetLeft(drone, horPos);
                        }
                        break;
                }

            });
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (_firstSeen)
            {
                _firstSeen = false;
                _canvasWidth = canvas.ActualWidth;
                _canvasHeight = canvas.ActualHeight;
                // make it into the center
                Canvas.SetLeft(drone, _canvasWidth / 2d - DRONE_WIDTH / 2d);
                Canvas.SetTop(drone, _canvasHeight / 2d - DRONE_HEIGHT / 2d);
            }
        }

    }
}
