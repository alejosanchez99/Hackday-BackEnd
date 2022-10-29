namespace Atom.Business
{
    using System.Text.Json;
    using Atom.Entities;
    using Newtonsoft.Json.Linq;

    public class KonectaAdapter
    {
        private string konectaUrl = "https://mqjl9s6vf4.execute-api.eu-west-1.amazonaws.com/prod/v1/hackday/public/event";
        private string apiUrlUsers = "https://sockethackday.azurewebsites.net/api/userhub/users";
        private string apiUrlMetric = "https://sockethackday.azurewebsites.net/api/metrichub/{0}";


        private Event GetDataAsync(Api api)
        {
            string responseText = api.InvokeIntegrationAsync<OnCreateHackathonEvents>(konectaUrl).Result;

            JObject json = JObject.Parse(responseText);

            JToken? entity = json["payload"]["data"]["onCreateHackathonEvents"];

            OnCreateHackathonEvents? res = JsonSerializer.Deserialize<OnCreateHackathonEvents>(entity.ToString());

            return JsonSerializer.Deserialize<Event>(res.Event);
        }

        public async void Processs()
        {
            Api api = new Api();

            Event events = GetDataAsync(api);

            List<Metric> metrics = AdaptEventMatrics(events);

            foreach (Metric metric in metrics)
            {
                string urlMetrics = string.Format(apiUrlMetric, metric.title);
                await api.InvokeIntegrationAsync(metric, urlMetrics);
            }

            List<User> users = AdaptEventUsers(events);
            await api.InvokeIntegrationAsync(users, apiUrlUsers);
        }


        private List<Metric> AdaptEventMatrics(Event events)
        {
            return (from eventInfo in events.detail.Event[1].detail.eventBody.data.metrics
                    select new Metric
                    {
                        count = eventInfo.stats.count,
                        title = eventInfo.metric,
                        max = eventInfo.stats.max,
                        min = eventInfo.stats.min,
                        sum = eventInfo.stats.sum
                    }).ToList();
        }

        private List<User> AdaptEventUsers(Event events)
        {
            return (from eventInfo in events.detail.Event.FirstOrDefault().detail.eventBody.service.users

                    select new User
                    {
                        id = eventInfo.id,
                        name = eventInfo.name
                    }).ToList();
        }
    }
}
