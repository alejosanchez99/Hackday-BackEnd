using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Atom.Entities
{
    public class OnCreateHackathonEvents
    {
        public string id { get; set; }
        public string createdAt { get; set; }
        [JsonProperty("event")]
        public Event Event { get; set; }
    }

    public class Data
    {
        public List<Metric> metrics { get; set; }
    }

    public class Detail
    {
        public string topicName { get; set; }
        public string version { get; set; }
        public EventBody eventBody { get; set; }
    }

    public class EventBody
    {
        public Service service { get; set; }
        public Data data { get; set; }
    }

    public class Event
    {
        public string version { get; set; }
        public string id { get; set; }

        [JsonProperty("detail-type")]
        public string DetailType { get; set; }
        public string source { get; set; }
        public string account { get; set; }
        public int time { get; set; }
        public string region { get; set; }
        public List<object> resources { get; set; }
        public Detail detail { get; set; }
    }

    public class Service
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<User> users { get; set; }
    }

    public class Stats
    {
        public int count { get; set; }
        public int sum { get; set; }
        public int? min { get; set; }
        public int? max { get; set; }
    }
}
