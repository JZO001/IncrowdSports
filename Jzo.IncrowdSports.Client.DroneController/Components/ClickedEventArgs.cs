using Jzo.IncrowdSports.Shared.Messages;
using System;

namespace Jzo.IncrowdSports.Client.DroneController.Components
{

    /// <summary>Represents the click event arguments when moving</summary>
    public class ClickedEventArgs : EventArgs
    {

        /// <summary>Initializes a new instance of the <see cref="ClickedEventArgs" /> class.</summary>
        /// <param name="direction">The direction.</param>
        public ClickedEventArgs(MoveDirection2DEnum direction)
        {
            Direction = direction;
        }

        /// <summary>Gets the direction.</summary>
        /// <value>The direction.</value>
        public MoveDirection2DEnum Direction { get; private set; }

    }

}
