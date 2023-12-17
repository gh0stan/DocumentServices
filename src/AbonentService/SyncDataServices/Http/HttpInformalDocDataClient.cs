using AbonentService.Dtos;
using System.Text;
using System.Text.Json;

namespace AbonentService.SyncDataServices.Http
{
    public class HttpInformalDocDataClient : IInformalDocDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HttpInformalDocDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task SendAbonentToInformalDocService(AbonentReadDto abonentReadDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(abonentReadDto),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_configuration["InformalDocumentService"], httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("DEBUG. Sync POST to InformalDocumentService OK.");
            }
            else 
            {
                Console.WriteLine("ERROR. Sync POST to InformalDocumentService NOT OK.");
            }
        }
    }
}
