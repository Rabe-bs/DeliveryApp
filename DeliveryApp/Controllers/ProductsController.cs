using DeliveryApp.Application.Features.Products.Commands.CreateProduct;
using DeliveryApp.Application.Features.Products.Commands.DeleteProduct;
using DeliveryApp.Application.Features.Products.Queries.GetProductsByRestaurantId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            var productId = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetByRestaurantId), new { restaurantId = command.RestaurantId }, productId);
        }

        [HttpGet("restaurant/{restaurantId:guid}")]
        public async Task<IActionResult> GetByRestaurantId(Guid restaurantId, CancellationToken cancellationToken)
        {
            var query = new GetProductsByRestaurantIdQuery(restaurantId);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _sender.Send(new DeleteProductCommand(id), cancellationToken);
            return NoContent();
        }
    }
}