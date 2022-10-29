namespace Atom.Socket.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class UserHub : Hub
    {
        public string Name { get; set; }
    }
}
