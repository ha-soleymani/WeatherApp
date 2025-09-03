namespace Location.API.Options
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        
        public string Database { get; set; } = "WeatherDb";
        
        public string Collection { get; set; } = "Locations";
    }
}
