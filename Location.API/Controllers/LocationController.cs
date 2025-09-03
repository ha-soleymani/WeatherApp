using Location.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Location.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationService locationService, ILogger<LocationController> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        [HttpPost()]
        public async Task<IActionResult> SaveAsync([FromBody] string cityName)
        {
            _logger.LogInformation("Received request to save location: {CityName}", cityName);

            try
            {
                await _locationService.SaveLocationAsync(cityName);
                _logger.LogInformation("Successfully saved location: {CityName}", cityName);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving location: {CityName}", cityName);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Received request to get all locations");
            var locations = await _locationService.GetLocationsAsync();

            _logger.LogInformation("Retrived all saved locations {locations}", locations);

            return Ok(locations);
        }
    }
}
