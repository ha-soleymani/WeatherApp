
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Weather.API.Controllers;
using Weather.API.Domain;
using Weather.API.Services;

namespace Weather.API.Tests
{
    public class WeatherControllerTests
    {
        private readonly Mock<ILogger<WeatherController>> _loggerMock = new();

        [Fact]
        public async Task Get_ReturnsOk_WithWeatherInfo()
        {
            var mockService = new Mock<IWeatherService>();
            mockService.Setup(s => s.GetWeatherAsync("Sydney"))
                .ReturnsAsync(new WeatherInfo
                {
                    CityName = "Sydney",
                    Temperature = 18.5m,
                    Description = "cloudy"
                });

            var controller = new WeatherController(mockService.Object, _loggerMock.Object);
            var result = await controller.Get("Sydney") as OkObjectResult;

            result.Should().NotBeNull();
            var weather = result.Value as WeatherInfo;
            weather.CityName.Should().Be("Sydney");
            weather.Temperature.Should().Be(18.5m);
            weather.Description.Should().Be("cloudy");
        }
    }
}
