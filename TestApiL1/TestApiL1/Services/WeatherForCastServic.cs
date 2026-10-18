 namespace TestApiL1.Services
{
 

    public class WeatherForCastServic : IWeatherForCastServic
    {

        private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
      };
        private readonly ILogger<WeatherForCastServic> _logger;

        public WeatherForCastServic(ILogger<WeatherForCastServic> logger)
        {
            _logger = logger;
        }
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            _logger.LogInformation("Getting ForeCast Data");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
