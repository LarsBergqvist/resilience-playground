namespace App.Services;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>?> GetWeatherForecastsAsync();
}

public class WeatherForecastService : IWeatherForecastService
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<WeatherForecast>?> GetWeatherForecastsAsync()
    {
        var response = await _httpClient.GetAsync("/weatherforecast"); 
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        return result;
    }
    
}