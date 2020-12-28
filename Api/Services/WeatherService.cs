using Api.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Api.Services
{
    public class WeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;        

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;            
        }

        public async Task<WeatherForecast[]> GetWeatherForecastsAsync()
        {
            var httpclient = _httpClientFactory.CreateClient("externalApi");
            return await httpclient.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");
        }
    }
}
