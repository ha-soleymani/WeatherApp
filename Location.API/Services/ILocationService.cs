using Location.API.Domain;

namespace Location.API.Services
{
    public interface ILocationService
    {
        /// <summary>
        /// Save Location
        /// </summary>
        /// <param name="cityName">
        /// Name of the city to be saved
        /// </param>
        /// <returns></returns>
        Task SaveLocationAsync(string cityName);
        
        /// <summary>
        /// Get Locations
        /// </summary>
        /// <returns></returns>
        Task<List<SavedLocation>> GetLocationsAsync();
    }
}
