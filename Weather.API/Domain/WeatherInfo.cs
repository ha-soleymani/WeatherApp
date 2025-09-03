namespace Weather.API.Domain
{
    public class WeatherInfo
    {
        public string CityName { get; set; }
        public decimal Temperature { get; set; }
        public string Description { get; set; }
        public DateTime RetrievedAt { get; set; }
    }
}
