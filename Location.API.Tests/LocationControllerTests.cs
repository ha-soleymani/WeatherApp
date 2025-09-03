using Location.API.Controllers;
using Location.API.Domain;
using Location.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Weather.API.Controllers;

public class LocationControllerTests
{
    private readonly Mock<ILocationService> _mockService;
    private readonly LocationController _controller;
    private readonly Mock<ILogger<LocationController>> _loggerMock = new();

    public LocationControllerTests()
    {
        _mockService = new Mock<ILocationService>();
        _controller = new LocationController(_mockService.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task SaveLocation_returns_created_result()
    {
        // Arrange
        var location = "Sydney" ;

        // Act
        var result = await _controller.SaveAsync(location);

        // Assert
        var createdResult = Assert.IsType<OkResult>(result);
        _mockService.Verify(r => r.SaveLocationAsync(location), Times.Once);
    }

    [Fact]
    public async Task GetAllLocations_returns_ok_with_locations()
    {
        // Arrange
        var locations = new List<SavedLocation>
        {
            new SavedLocation { CityName = "Melbourne" },
            new SavedLocation { CityName = "Brisbane" }
        };

        _mockService.Setup(r => r.GetLocationsAsync()).ReturnsAsync(locations);

        // Act
        var result = await _controller.GetAllAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedList = Assert.IsType<List<SavedLocation>>(okResult.Value);
        Assert.Equal(2, returnedList.Count);
        Assert.Contains(returnedList, l => l.CityName == "Melbourne");
    }
}


