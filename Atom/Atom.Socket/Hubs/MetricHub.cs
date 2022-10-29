namespace Atom.Socket.Hubs
{
    using Atom.Entities;
    using Microsoft.AspNetCore.SignalR;

    public class MetricHub : Hub
    {
        public List<Metric> Metrics { get; set; }
    }
}
