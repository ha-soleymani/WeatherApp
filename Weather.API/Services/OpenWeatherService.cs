using Microsoft.Extensions.Options;
using System.Text.Json;
using Weather.API.Domain;
using Weather.API.Options;

namespace Weather.API.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherOptions _options;
        private readonly ILogger<OpenWeatherService> _logger;


        public OpenWeatherService(HttpClient httpClient, IOptions<OpenWeatherOptions> options, 
            ILogger<OpenWeatherService> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;

        }

        public async Task<WeatherInfo> GetWeatherAsync(string cityName)
        {
            var url = $"{_options.BaseUrl}?q={cityName}&appid={_options.ApiKey}&units=metric";

            _logger.LogInformation("Fetching weather data for {CityName} from URL: {Url}", cityName, url);

            try
            {
                var response = await _httpClient.GetFromJsonAsync<JsonElement>(url);

                var temperature = response.GetProperty("main").GetProperty("temp").GetDecimal();
                var description = response.GetProperty("weather")[0].GetProperty("description").GetString();

                var weatherInfo = new WeatherInfo
                {
                    CityName = cityName,
                    Temperature = temperature,
                    Description = description,
                    RetrievedAt = DateTime.UtcNow
                };

                _logger.LogInformation("Weather data retrieved for {CityName}: {@WeatherInfo}", cityName, weatherInfo);
                return weatherInfo;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request failed while fetching weather for {CityName}", cityName);
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Unexpected JSON structure while parsing weather data for {CityName}", cityName);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error occurred while retrieving weather for {CityName}", cityName);
                throw;
            }
        }
    }
}
