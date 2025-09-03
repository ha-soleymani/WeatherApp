using Common.Utility;
using Serilog;
using Weather.API.Options;
using Weather.API.Services;

var builder = WebApplication.CreateBuilder(args);

LoggingConfigurator.ConfigureLogging();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<IWeatherService, OpenWeatherService>();
builder.Services.Configure<OpenWeatherOptions>(
    builder.Configuration.GetSection("OpenWeatherMap")
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
