using System.ComponentModel;

namespace Jzo.IncrowdSports.Client.DroneController.Components
{

    public class NaviControlViewModel : INotifyPropertyChanged, INaviControlViewModel
    {

        private bool mIsEnabled = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public NaviControlViewModel()
        {
        }

        public bool IsEnabled
        {
            get { return mIsEnabled; }
            set
            {
                mIsEnabled = value;
                RaisePropertyChangedEvent(nameof(IsEnabled));
            }
        }

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
