using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Models;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Route("{city:alpha}/{days:int}")]
        public IEnumerable<WeatherForecastDto> Get(string city, int days)
        {
            _logger.LogInformation("Forecast for {days} days in {city}", days, city);

            return Enumerable.Range(1, days).Select(index => new WeatherForecastDto
            {
                City = city,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
