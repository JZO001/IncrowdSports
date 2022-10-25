using Jzo.IncrowdSports.Client.DroneController.Components;
using Jzo.IncrowdSports.Client.DroneController.Configuration;
using Jzo.IncrowdSports.Client.DroneController.Data;
using Jzo.IncrowdSports.Shared;
using Jzo.IncrowdSports.Shared.Codes;
using Jzo.IncrowdSports.Shared.Messages;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Jzo.IncrowdSports.Client.DroneController.ViewModels
{

    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private readonly MessageFormatter<MessageMove> _messageFormatter = new ();
        private readonly NetworkConfig _networkConfig;
        private TcpClient? _tcpClient;
        private NetworkStream? _networkStream;

        private ConnectionStateEnum _isConnected = ConnectionStateEnum.Disconnected;
        private int _speedStep = 10;

        private AsyncCommand? mConnectCommand;
        private GenericCommand? mMoveDirectionCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .Build();

            _networkConfig = configuration.GetSection("NetworkConfig").Get<NetworkConfig>();
        }

        public ConnectionStateEnum ConnectionState
        { 
            get => _isConnected; 
            set 
            { 
                _isConnected = value;
                RaisePropertyChangedEvent(nameof(ConnectionState));
            }
        }

        public int SpeedStep
        {
            get => _speedStep;
            set
            {
                if (value > 0)
                {
                    _speedStep = value;
                    RaisePropertyChangedEvent(nameof(SpeedStep));
                }
            }
        }

        public AsyncCommand ConnectCommand
        {
            get
            {
                return mConnectCommand ?? (mConnectCommand = new AsyncCommand(ConnectCommandHandler, () => true));
            }
        }

        public GenericCommand MoveDirectionCommand
        {
            get
            {
                return mMoveDirectionCommand ?? (mMoveDirectionCommand = new GenericCommand(MoveDirectionCommandHandler, (object o) => true));
            }
        }

        private async Task ConnectCommandHandler()
        {
            if (ConnectionState == ConnectionStateEnum.Disconnected)
            {
                ConnectionState = ConnectionStateEnum.Connecting;

                _tcpClient = new TcpClient();
                try
                {
                    await _tcpClient.ConnectAsync(_networkConfig.RemoteIP, _networkConfig.RemotePort);
                    _networkStream = _tcpClient.GetStream();

                    ConnectionState = ConnectionStateEnum.Connected;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{this.GetType().Name} : {ex.Message}");
                    _tcpClient.Dispose();
                    _tcpClient = null;
                    ConnectionState = ConnectionStateEnum.Disconnected;
                }
            }
            else
            {
                RecycleNetworkObjects();
                ConnectionState = ConnectionStateEnum.Disconnected;
            }
        }

        private void MoveDirectionCommandHandler(object parameters)
        {
            ClickedEventArgs clickedEventArgs = parameters as ClickedEventArgs;
            MessageMove messageMove = new MessageMove() { Direction = clickedEventArgs.Direction, SpeedStep = SpeedStep };
            try
            {
                _messageFormatter.Write(_networkStream, messageMove);
                _networkStream.Flush();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{this.GetType().Name} : {ex.Message}");
                RecycleNetworkObjects();
                ConnectionState = ConnectionStateEnum.Disconnected;
            }
        }

        private void RecycleNetworkObjects()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _networkStream.Close();
            _networkStream.Dispose();
            _tcpClient.Dispose();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            _networkStream = null;
            _tcpClient = null;
        }

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
