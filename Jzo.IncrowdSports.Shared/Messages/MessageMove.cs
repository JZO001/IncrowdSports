namespace Jzo.IncrowdSports.Shared.Messages
{

    /// <summary>Represents the ordered move message</summary>
    public class MessageMove : MessageBase
    {

        public MoveDirection2DEnum Direction { get; set; } = MoveDirection2DEnum.Forward;

        public int SpeedStep { get; set; }

    }

}
