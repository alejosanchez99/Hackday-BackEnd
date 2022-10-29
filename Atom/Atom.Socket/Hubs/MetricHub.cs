namespace Atom.Socket.Hubs
{
    using Atom.Entities;
    using Microsoft.AspNetCore.SignalR;

    public class MetricHub : Hub
    {
        public Metric Metric { get; set; }
    }
}
