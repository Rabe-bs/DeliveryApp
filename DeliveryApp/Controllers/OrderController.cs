using Application.Features.Orders.Commands.UpdateOrderStatus;
using DeliveryApp.Application.Features.Orders.Commands.CreateOrder;
using DeliveryApp.Application.Features.Orders.Queries.GetOrderById;
using DeliveryApp.DeliveryApp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreaeOrder([FromBody] CreateOrderCommand command , CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var customerId))
            {
                return Unauthorized(new { message = "Invalid user token or unauthorized access." });
            }
            var secureCommand = new CreateOrderCommand(
                customerId,
                command.RestaurantId,
                command.TotalAmount,
                command.DeliveryAddress
            );
            var orderId = await _mediator.Send(secureCommand, cancellationToken);

            return Ok(new { orderId, message = "Order created successfully and wallet balance deducted." });


        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            { return NotFound(new { message = "Order not found." });}

            return Ok(result);
        }

        [HttpPut("{id:guid}/status")]

        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatus newStatus , CancellationToken cancellationToken)
        {
            var comand = new UpdateOrderStatusCommand(id, newStatus);
            var result = await _mediator.Send(comand, cancellationToken);

            if(!result)
            { return BadRequest(new { message = "Failed To Update Order Staatus " }); }

            return Ok(new { message = $" Order Status successfully  Updated to {newStatus} " });
        }
    }
}
