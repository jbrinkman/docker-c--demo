using System.Text.Json;
using webapi;

public interface IWeatherService
{
    IEnumerable<WeatherForecast> GetWeather();
}

public class WeatherService : IWeatherService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherService> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public WeatherService(ILogger<WeatherService> logger, IHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public IEnumerable<WeatherForecast> GetWeather()
    {
        IEnumerable<WeatherForecast> forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });

        string dataPath = Path.Combine(_hostEnvironment.ContentRootPath, "data");
        if (!Path.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
        string fileName = $"{dataPath}/WeatherForecast{DateTime.UtcNow.ToFileTime()}.json";
        string jsonString = JsonSerializer.Serialize(forecast);
        File.WriteAllText(fileName, jsonString);

        return forecast;

    }
}
