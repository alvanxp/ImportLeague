using AutoMapper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ImportLeague.Infrastructure
{
    public abstract class BaseExternalService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;
        public BaseExternalService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            Mapper = mapper;
            _baseUrl = "http://api.football-data.org/v2/";
        }

        protected async Task<T> Get<T>(string path, CancellationTokenSource cancellationToken) where T : class
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", "369fb5a1945d49209c6923ef7e9672bb");
            var response = await client.GetAsync($"{_baseUrl}{path}", cancellationToken.Token);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            cancellationToken.Cancel();
            response.EnsureSuccessStatusCode();
            return null;
        }
        protected IMapper Mapper { get; }
    }
}
