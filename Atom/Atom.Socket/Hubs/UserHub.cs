namespace Atom.Socket.Hubs
{
    using Atom.Entities;
    using Microsoft.AspNetCore.SignalR;

    public class UserHub : Hub
    {
        public List<User> Users { get; set; }
    }
}
