using Jzo.IncrowdSports.Server.Drone.Data;
using System;

namespace Jzo.IncrowdSports.Server.Drone.ViewModels
{

    public interface IMainWindowViewModel
    {

        event EventHandler<MoveEventArgs>? EventMessageMove;

    }

}
