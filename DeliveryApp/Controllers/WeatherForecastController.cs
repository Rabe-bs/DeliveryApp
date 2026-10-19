using DeliveryApp.Application.Features.Orders.Commands.CreateOrder;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IValidator<CreateOrderCommandValidator > _validator;
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var command = new CreateOrderCommandValidator();
         var result =    _validator.Validate(command);

          var x =  result.IsValid;


            // How To use Fluent Validator with BehaviorPipeline
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
