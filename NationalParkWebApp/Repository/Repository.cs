using NationalParkWebApp.Repository.IRepository;
using Newtonsoft.Json;
using System.Text;

namespace NationalParkWebApp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAync(string url, T objToCreate)
        {
            var requests = new HttpRequestMessage (HttpMethod.Post, url);
            if (objToCreate != null)
            {
                requests.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
                var client = _httpClientFactory.CreateClient();
                HttpResponseMessage httpResponse = await client.SendAsync(requests);
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.Created)

                    return true ; 
            }
            return false;        
        }

        public async Task<bool> DeleteAsync(string url, int id)
        {
            {
                var requests = new HttpRequestMessage(HttpMethod.Delete, url + "/" + id.ToString());
              
               
                    var client = _httpClientFactory.CreateClient();
                    HttpResponseMessage httpResponse = await client.SendAsync(requests);
                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.Created)

                        return true;
            }
                return false;
        }
        

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var requests = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await client.SendAsync(requests);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonString = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            return null;
        }

        public async Task<T> GetAsync(string url, int id)
        {
            var requests = new HttpRequestMessage(HttpMethod.Get, url + "/" + id.ToString());
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await client.SendAsync(requests);
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonString = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }

        public async Task<bool> UpdateAsync(string url, T objToUpdate)
        {
            var requests = new HttpRequestMessage(HttpMethod.Put, url);
            if (objToUpdate != null)
            {
                requests.Content = new StringContent(JsonConvert.SerializeObject(objToUpdate)
                    , Encoding.UTF8, "application/json");
                var client = _httpClientFactory.CreateClient();
                HttpResponseMessage httpResponse = await client.SendAsync(requests);
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return true;
            }
            return false;
        }
    }
}
