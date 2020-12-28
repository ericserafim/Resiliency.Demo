using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(WeatherService weatherService, ILogger<WeatherForecastController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Ok(await _weatherService.GetWeatherForecastsAsync());
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(500, "External weather service is inoperative, please try later on. (due to Circuit-Breaker)");
            }
        }
    }
}
