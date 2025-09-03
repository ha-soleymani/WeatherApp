using Common.Utility;
using Location.API.Infrastructure;
using Location.API.Options;
using Location.API.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Serilog;
using Serilog.Extensions.Hosting;

LoggingConfigurator.ConfigureLogging();
var builder = WebApplication.CreateBuilder(args);

// Register Guid serializer
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

// MongoDB config
builder.Services.Configure<MongoDbOptions>(
    builder.Configuration.GetSection("MongoDB")
);

// Register LocationService
builder.Services.AddSingleton<ILocationService, MongoLocationService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use Serilog for logging
builder.Host.UseSerilog();

// Register DiagnosticContext properly
builder.Services.AddSingleton(provider =>
{
    var logger = provider.GetRequiredService<Serilog.ILogger>();
    return new DiagnosticContext(logger);
});

// Use Serilog for logging
builder.Host.UseSerilog();

// Register Serilog.ILogger explicitly
builder.Services.AddSingleton(Log.Logger);

// Register DiagnosticContext with its dependency
builder.Services.AddSingleton(provider =>
{
    var logger = provider.GetRequiredService<Serilog.ILogger>();
    return new DiagnosticContext(logger);
});

var app = builder.Build();

// Use Serilog request logging
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging(); // Logs HTTP requests

app.Run();
