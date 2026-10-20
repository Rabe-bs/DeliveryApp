using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Application.Features.Restaurants.Commands.CreateRestaurant;
using DeliveryApp.Application.Features.Restaurants.Queries.GetRestaurantById;
using DeliveryApp.Application.Features.Restaurants.Queries.GetRestaurantsList;

namespace DeliveryApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ISender _sender;

        public RestaurantsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand command, CancellationToken cancellationToken)
        {
            var restaurantId = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = restaurantId }, restaurantId);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetRestaurantByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = new GetRestaurantsListQuery(pageNumber, pageSize);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}