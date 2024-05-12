using App.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IWeatherForecastService _weatherForecastService;

    public IndexModel(ILogger<IndexModel> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    public async Task OnGet()
    {
        var test = await _weatherForecastService.GetWeatherForecastsAsync(); 
    }
}