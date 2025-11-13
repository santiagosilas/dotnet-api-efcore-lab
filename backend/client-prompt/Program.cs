using System.Net.Http.Json;
// using System.Net.Http.Json;

var client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7181")
};

var forecasts = await client.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");

Console.WriteLine("Weather Forecasts:");
foreach (var forecast in forecasts!)
{
    Console.WriteLine($"{forecast.Date:yyyy-MM-dd} - {forecast.TemperatureC}°C - {forecast.Summary}");
}

public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}    