using Weather.API.Domain;

namespace Weather.API.Services
{
    public interface IWeatherService
    {
        /// <summary>
        /// Get city weather
        /// </summary>
        /// <param name="cityName">
        /// Name of the city
        /// </param>
        /// <returns></returns>
        Task<WeatherInfo> GetWeatherAsync(string cityName);
    }
}
