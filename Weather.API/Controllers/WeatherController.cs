using Microsoft.AspNetCore.Mvc;
using Weather.API.Services;

namespace Weather.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> Get(string cityName)
        {
            _logger.LogInformation("Received request to Search Weather for {CityName}", cityName);

            var weather = await _weatherService.GetWeatherAsync(cityName);
            return Ok(weather);
        }
    }
}
