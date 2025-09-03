using Location.API.Options;
using Microsoft.Extensions.Options;
using Weather.API.Options;

namespace TestUtilities.Mocks
{
    public static class AppSettingsOptionsMock
    {
        public static IOptions<OpenWeatherOptions> OpenWeatherOptions
        { 
            get 
            {
                var settings = new OpenWeatherOptions()
                {
                    BaseUrl = "localhost",
                    ApiKey = "Test_API_Key"
                };
                return Options.Create(settings);
            } 
        }

        public static IOptions<MongoDbOptions> MongoDbOptions
        {
            get
            {
                var settings = new MongoDbOptions()
                {
                    Collection = "Locations",
                    ConnectionString = "mongodb://localhost:27017",
                    Database = "WeatherDb"
                };
                return Options.Create(settings);
            }
        }
    }
}
