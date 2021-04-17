using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TechnicalCore.Web.Models;

namespace TechnicalCore.Web.Clients
{
    public class ArticleHttpClient
    {
        private readonly HttpClient _httpClient;

        public ArticleHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<ArticlesContainer>> GetArticles()
        {
            var response = await _httpClient.GetAsync(@"?query= 
            { articles 
                { id name price rating } 
            }");
            string stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ArticlesContainer>>(stringResult);
        }
    }
}
