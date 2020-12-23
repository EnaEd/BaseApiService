using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BaseApiService
{
    public class BaseApiService
    {
        /// <summary>
        /// http client
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// default ctor
        /// </summary>
        public BaseApiService() => _httpClient = GetHttpClient();

        /// <summary>
        /// ctor for httplcient custom setup 
        /// </summary>
        /// <param name="client">clinet with differnt settings</param>
        public BaseApiService(HttpClient client) => _httpClient = client;

        /// <summary>
        /// ctor for set base url
        /// </summary>
        /// <param name="baseUrl">common path to api</param>
        public BaseApiService(string baseUrl) => _httpClient = GetHttpClient(baseUrl);

        /// <summary>
        /// get deserilized data from "GET" request
        /// </summary>
        /// <typeparam name="T">ur data model to return</typeparam>
        /// <param name="pathToEndpoint">part different path to api endpint</param>
        /// <returns>object by T type</returns>
        public async Task<T> GetAsync<T>(string pathToEndpoint)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(pathToEndpoint);
            string responseResult = await responseMessage.Content.ReadAsStringAsync();
            var desirializedResult = JsonConvert.DeserializeObject<T>(responseResult);

            return desirializedResult;
        }

        /// <summary>
        /// get full response message from "GET" request
        /// </summary>
        /// <param name="pathToEndpoint">part different path to api endpint</param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> ExecuteGetAsync(string pathToEndpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(pathToEndpoint);
            return response;
        }

        /// <summary>
        /// get deserilized data from "POST" request
        /// </summary>
        /// <typeparam name="T">ur data model to return</typeparam>
        /// <param name="pathToEndpoint">part different path to api endpint</param>
        /// <param name="data">request model</param>
        /// <returns>object by T type</returns>
        public async Task<T> PostAsync<T>(string pathToEndpoint, object data)
        {
            var content = JsonConvert.SerializeObject(data);
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(pathToEndpoint, new StringContent(content, Encoding.Unicode, "application/json"));
            string responseResult = await responseMessage.Content.ReadAsStringAsync();
            var desirializedResult = JsonConvert.DeserializeObject<T>(responseResult);

            return desirializedResult;
        }

        /// <summary>
        /// get full response message from "POST" request
        /// </summary>
        /// <param name="pathToEndpoint">part different path to api endpint</param>
        /// <param name="data">request model</param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> ExecutePostAsync(string pathToEndpoint, object data)
        {
            var content = JsonConvert.SerializeObject(data);
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(pathToEndpoint, new StringContent(content, Encoding.Unicode, "application/json"));

            return responseMessage;
        }

        private HttpClient GetHttpClient(string baseUrl = null)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                client.BaseAddress = new Uri(baseUrl);
            }
            return client;
        }

    }
}
