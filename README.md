# IncrowdSports
IncrowdSports is a demo implementation. Consists from two applications, a client (remote controller) and a server (drone simulator).


## Content

There are three types of projects are exist in the solution:
- Jzo.IncrowdSports.Client.DroneController
- Jzo.IncrowdSports.Server.Drone
- Jzo.IncrowdSports.Shared


## Content - Jzo.IncrowdSports.Client.DroneController
A client to control the drone image


## Jzo.IncrowdSports.Server.Drone
A server app which receives the signals from the connected client, display and move the drone on the screen


## Jzo.IncrowdSports.Shared
A shared code-base for the projects above


## Configuration of Jzo.IncrowdSports.Client.DroneController
Open the appsettings.json file and modify the remote address (IP) and port to connect to.


## Configuration of Jzo.IncrowdSports.Server.Drone
Open the appsettings.json file and modify the local port where to listen to the incoming clients.


## Startup
It is recommended to configure the solution startup preferences. Right click on the solution and select "Set Startup Projects..." from the context menu. Choose the "Multiple startup projects",
and set the Action to "Start" for the project "Jzo.IncrowdSports.Client.DroneController" and for "Jzo.IncrowdSports.Server.Drone".
