using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Entities
{
    public class Request
    {
        public string id { get; set; }
        public string type { get; set; }
        public Payload payload { get; set; }
    }

    public class Payload
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public OnCreateHackathonEvents onCreateHackathonEvents { get; set; }
    }

    public class OnCreateHackathonEvents
    {
        public string id { get; set; }
        public string createdAt { get; set; }
        public string @event { get; set; }
    }

   



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
}
