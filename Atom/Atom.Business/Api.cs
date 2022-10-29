namespace Atom.Business
{
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class Api
    {

        public async Task<bool> InvokeIntegrationAsync<T>(T objectTransferencia, string url)
            where T : class, new()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            bool y = false;
            using (HttpClient httpCliente = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(objectTransferencia);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpCliente.PostAsync(url, data).ConfigureAwait(false);

                y = response.IsSuccessStatusCode;
            }

            return y;
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<string> InvokeIntegrationAsync<TR>(string url)
        {
            string responseText = string.Empty;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (HttpClient httpCliente = new HttpClient())
            {
                HttpResponseMessage response = await httpCliente.GetAsync(url).ConfigureAwait(false);

                responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return responseText;
        }
    }
}
