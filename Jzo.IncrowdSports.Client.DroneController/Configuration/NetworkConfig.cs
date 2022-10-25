namespace Jzo.IncrowdSports.Client.DroneController.Configuration
{

    /// <summary>Represents the network configuration model</summary>
    public class NetworkConfig
    {

        /// <summary>Gets or sets the remote ip.</summary>
        /// <value>The remote ip.</value>
        public string RemoteIP { get; set; } = "localhost";

        /// <summary>Gets or sets the remote port.</summary>
        /// <value>The remote port.</value>
        public int RemotePort { get; set; } = 10000;

    }

}
