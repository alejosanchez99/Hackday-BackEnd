namespace Atom.Entities
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class OnCreateHackathonEvents
    {
        public string id { get; set; }
        public string createdAt { get; set; }
        [JsonPropertyName("event")]
        public string Event { get; set; }
    }

    public class Data
    {
        public List<Metric> metrics { get; set; }
    }

    public class Detail
    {
        [JsonPropertyName("events")]
        public List<Event> Event { get; set; }
        public string topicName { get; set; }
        public string version { get; set; }
        [JsonPropertyName("eventBody")]
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

        [JsonPropertyName("detail-type")]
        public string DetailType { get; set; }
        public string source { get; set; }
        public string account { get; set; }
        public string region { get; set; }
        public List<object> resources { get; set; }
        [JsonPropertyName("detail")]
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
