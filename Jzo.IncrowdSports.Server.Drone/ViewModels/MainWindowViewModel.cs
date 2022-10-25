using Jzo.IncrowdSports.Server.Drone.Configuration;
using Jzo.IncrowdSports.Server.Drone.Data;
using Jzo.IncrowdSports.Shared;
using Jzo.IncrowdSports.Shared.Messages;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Jzo.IncrowdSports.Server.Drone.ViewModels
{

    public class MainWindowViewModel : INotifyPropertyChanged, IMainWindowViewModel
    {

        private readonly NetworkConfig _networkConfig;
        private readonly MessageFormatter<MessageMove> _messageFormatter = new();
        private TcpListener? _tcpListener;
        private Thread? _acceptThread;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<MoveEventArgs>? EventMessageMove;

        public MainWindowViewModel()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .Build();

            _networkConfig = configuration.GetSection("NetworkConfig").Get<NetworkConfig>();
            InitializeTcpListener();
        }

        private void InitializeTcpListener()
        {
            _acceptThread = new Thread(new ThreadStart(DoAcceptThread));
            _acceptThread.IsBackground = true;
            _acceptThread.Name = "Listen and accept incoming connections";
            _acceptThread.Start();
        }

        private bool StartTcpListener()
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Any, _networkConfig.LocalPort);
                _tcpListener.Start();
                return true;
            }
            catch (Exception ex)
            {
                // unable to start listener, port is occupied?
                Debug.WriteLine($"{this.GetType().Name} : {ex.Message}");
            }
            return false;
        }

        private void DoAcceptThread()
        {
            bool isTcpListenerRunning = StartTcpListener();
            while (isTcpListenerRunning)
            {
                TcpClient? tcpClient = null;
                try
                {
                    tcpClient = _tcpListener.AcceptTcpClient();
                }
                catch (Exception ex)
                {
                    // unable to accept incoming connection
                    Debug.WriteLine($"{this.GetType().Name} : {ex.Message}");
                }
                if (tcpClient != null)
                {
                    _tcpListener.Stop(); // only one controller at the same time
                    NetworkStream networkStream = tcpClient.GetStream();
                    try
                    {
                        while (true)
                        {
                            MessageMove message = _messageFormatter.Read(networkStream);
                            RaiseMessageMoveEvent(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        // unable to process data from socket/stream. Maybe disconnected, or invalid message caused.
                        Debug.WriteLine($"{this.GetType().Name} : {ex.Message}");
                        tcpClient.Close();
                        tcpClient.Dispose();
                        networkStream.Dispose();
                    }
                    isTcpListenerRunning = StartTcpListener();
                }
            }
        }

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RaiseMessageMoveEvent(MessageMove message)
        {
            EventMessageMove?.Invoke(this, new MoveEventArgs(message));
        }

    }

}
