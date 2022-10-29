namespace Atom.Api.RequestApi
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class Api
    {

        public async Task<bool> InvokeIntegrationAsync<T>(T objectTransferencia, string url)
            where T : class, new()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            bool y = false;
            using (var httpCliente = new HttpClient())
            {
                //AddHeader(httpCliente);

                var json = JsonConvert.SerializeObject(objectTransferencia);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpCliente.PostAsync(url, data).ConfigureAwait(false);

                y = response.IsSuccessStatusCode;
            }

            return y;
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<TR>(string url)
            where TR : class, new()
        {
            TR objResponseApi = null;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var httpCliente = new HttpClient())
            {
                var response = await httpCliente.GetAsync(url).ConfigureAwait(false);

                var y =  await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                objResponseApi =  JsonConvert.DeserializeObject<TR>(y);
            }

            return objResponseApi;
        }
    }
}
