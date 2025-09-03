using Location.API.Domain;
using Location.API.Infrastructure;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using System.Linq.Expressions;

public class MongoLocationServiceTests
{
    private readonly Mock<IMongoCollection<SavedLocation>> _collectionMock = new();
    private readonly Mock<ILogger<MongoLocationService>> _loggerMock = new();
    private readonly MongoLocationServiceForTest _service;

    public MongoLocationServiceTests()
    {
        _service = new MongoLocationServiceForTest(_collectionMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task SaveLocationAsync_InsertsDocument_AndLogsSuccess()
    {
        _collectionMock
            .Setup(c => c.InsertOneAsync(It.IsAny<SavedLocation>(), null, default))
            .Returns(Task.CompletedTask);

        await _service.SaveLocationAsync("Sydney");

        _collectionMock.Verify(c => c.InsertOneAsync(
            It.Is<SavedLocation>(l => l.CityName == "Sydney" && l.Id != Guid.Empty),
            null,
            default), Times.Once);

        _loggerMock.Verify(l => l.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Successfully saved location")),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }

    [Fact]
    public async Task SaveLocationAsync_LogsError_OnInsertFailure()
    {
        _collectionMock
            .Setup(c => c.InsertOneAsync(It.IsAny<SavedLocation>(), null, default))
            .ThrowsAsync(new MongoException("Insert failed"));

        await Assert.ThrowsAsync<MongoException>(() => _service.SaveLocationAsync("Melbourne"));

        _loggerMock.Verify(l => l.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Failed to save location")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }
}