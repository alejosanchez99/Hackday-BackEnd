namespace Atom.Business
{
    using Atom.Entities;

    public class KonectaAdapter
    {
        private string konectaUrl = "https://mqjl9s6vf4.execute-api.eu-west-1.amazonaws.com/prod/v1/hackday/public/event";
        private string apiUrlUsers = "https://sockethackday.azurewebsites.net/api/userhub/users";
        private string apiUrlMetric = "https://sockethackday.azurewebsites.net/api/metrichub/{0}";


        private async Task<OnCreateHackathonEvents> GetDataAsync(Api api)
        {
            return await api.InvokeIntegrationAsync<OnCreateHackathonEvents>(konectaUrl);
        }

        public async void Processs()
        {
            Api api = new Api();

            OnCreateHackathonEvents events = GetDataAsync(api).Result;

            List<Metric> metrics = AdaptEventMatrics(events);

            foreach (Metric metric in metrics)
            {
                string urlMetrics = string.Format(apiUrlMetric, metric.title);
                await api.InvokeIntegrationAsync(metric, urlMetrics);
            }

            List<User> users = AdaptEventUsers(events);
            await api.InvokeIntegrationAsync(users, apiUrlUsers);
        }


        private List<Metric> AdaptEventMatrics(OnCreateHackathonEvents events)
        {
            return (from eventInfo in events.Event.detail.eventBody.data.metrics
                    select new Metric
                    {
                        count = eventInfo.count,
                        title = eventInfo.title,
                        max = eventInfo.max,
                        min = eventInfo.min,
                        sum = eventInfo.sum
                    }).ToList();
        }

        private List<User> AdaptEventUsers(OnCreateHackathonEvents events)
        {
            return (from eventInfo in events.Event.detail.eventBody.service.users
                    select new User
                    {
                        id = eventInfo.id,
                        name = eventInfo.name
                    }).ToList();
        }
    }
}
