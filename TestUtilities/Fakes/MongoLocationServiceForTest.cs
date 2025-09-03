using Location.API.Domain;
using Location.API.Infrastructure;
using Location.API.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

public class MongoLocationServiceForTest : ILocationService
{
    private readonly IMongoCollection<SavedLocation> _collection;
    private readonly ILogger<MongoLocationService> _logger;

    public MongoLocationServiceForTest(IMongoCollection<SavedLocation> collection, ILogger<MongoLocationService> logger)
    {
        _collection = collection;
        _logger = logger;
    }

    public async Task SaveLocationAsync(string cityName)
    {
        var location = new SavedLocation
        {
            Id = Guid.NewGuid(),
            CityName = cityName,
            SavedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Attempting to save location: {@Location}", location);

        try
        {
            await _collection.InsertOneAsync(location);
            _logger.LogInformation("Successfully saved location: {CityName} with ID {Id}", cityName, location.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save location: {CityName}", cityName);
            throw;
        }
    }

    public async Task<List<SavedLocation>> GetLocationsAsync()
    {
        _logger.LogInformation("Retrieving saved locations from MongoDB");

        try
        {
            var locations = await _collection
                .Find(_ => true)
                .SortByDescending(l => l.SavedAt)
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} locations", locations.Count);
            return locations;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve saved locations");
            throw;
        }
    }
}