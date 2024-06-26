using App.Services;
using Microsoft.Extensions.Http.Resilience;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddHttpClient<IWeatherForecastService, WeatherForecastService>(
    configureClient: static client =>
    {
        client.BaseAddress = new("https://localhost:7205");
    })
    .AddResilienceHandler("RetryWeatherForecastService", configure =>
    {
        // Refer to https://www.pollydocs.org/strategies/retry.html#defaults for retry defaults
        configure.AddRetry(new HttpRetryStrategyOptions
        {
            MaxRetryAttempts = 4,
            Delay = TimeSpan.FromSeconds(2),
            BackoffType = DelayBackoffType.Exponential
        });
        configure.AddTimeout(TimeSpan.FromSeconds(5));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();