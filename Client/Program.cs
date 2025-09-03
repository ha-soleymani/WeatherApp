using Location.API.Domain;
using System.Net.Http.Json;
using Weather.API.Domain;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        while (true)
        {
            Console.WriteLine("1. Search Weather\n2. Save City\n3. View Saved Cities\n4. Exit");
           var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter city: ");
                var city = Console.ReadLine();
                var result = await httpClient.GetFromJsonAsync<WeatherInfo>($"http://localhost:5162/api/Weather/{city}");
                Console.WriteLine($"{result.CityName}: {result.Temperature}°C, {result.Description}");
            }
            else if (choice == "2")
            {
                Console.Write("Enter city to save: ");
                var city = Console.ReadLine();
                await httpClient.PostAsJsonAsync($"http://localhost:5128/api/Location", city);
                Console.WriteLine("Saved.");
            }
            else if (choice == "3")
            {
                var locations = await httpClient.GetFromJsonAsync<List<SavedLocation>>("http://localhost:5128/api/location");
                foreach (var loc in locations)
                    Console.WriteLine($"{loc.CityName} (saved at {loc.SavedAt})");
            }
            else break;
        }
    }
}