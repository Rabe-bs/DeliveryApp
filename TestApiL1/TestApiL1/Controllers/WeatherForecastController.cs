using Microsoft.AspNetCore.Mvc;
using TestApiL1.Services;

namespace TestApiL1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForCastServic _service;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForCastServic service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _service.GetForecasts();
        }
    }
}
