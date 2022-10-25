using Jzo.IncrowdSports.Shared.Messages;
using System;

namespace Jzo.IncrowdSports.Server.Drone.Data
{

    /// <summary>Represents the movement order event arguments</summary>
    public class MoveEventArgs : EventArgs
    {

        /// <summary>Initializes a new instance of the <see cref="MoveEventArgs" /> class.</summary>
        /// <param name="messageMove">The message move.</param>
        public MoveEventArgs(MessageMove messageMove)
        {
            MessageMove = messageMove;
        }

        /// <summary>Gets the message.</summary>
        /// <value>The message move.</value>
        public MessageMove MessageMove { get; private set; }

    }

}
