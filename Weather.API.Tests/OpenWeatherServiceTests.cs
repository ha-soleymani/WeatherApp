using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;
using TestUtilities.Mocks;
using Weather.API.Services;

namespace Weather.API.Tests
{
    public class OpenWeatherServiceTests
    {
        private readonly Mock<ILogger<OpenWeatherService>> _loggerMock = new();

        [Fact]
        public async Task GetWeatherAsync_ReturnsExpectedResult()
        {
            var mockHttp = new HttpMessageHandlerMock("""
                {
                    "main": { "temp": 25 },
                    "weather": [ { "description": "sunny" } ]
                }
                """);

            var client = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("https://api.openweathermap.org")
            };

            var service = new OpenWeatherService(client, AppSettingsOptionsMock.OpenWeatherOptions, _loggerMock.Object);
            var result = await service.GetWeatherAsync("Sydney");

            result.CityName.Should().Be("Sydney");
            result.Temperature.Should().Be(25);
            result.Description.Should().Be("sunny");
        }
    }
}