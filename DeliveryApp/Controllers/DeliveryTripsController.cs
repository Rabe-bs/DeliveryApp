using Application.Features.DeliveryTrips.Commands.UpdateTripStatus;
using DeliveryApp.Application.Features.DeliveryTrips.Queries.GetDeliveryTripById;
using DeliveryApp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTripsController : ControllerBase
    {
        private readonly ISender _sender;
        public DeliveryTripsController(ISender sender)
        {
            _sender = sender;

        }

        [HttpPut("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(
    Guid id,
    [FromBody] UpdateTripStatusRequest request,
    CancellationToken cancellationToken)
        {
            var command = new UpdateDeliveryTripStatusCommand(
                TripId: id,
                NewStatus: request.NewStatus,
                Latitude: request.Latitude,
                Longitude: request.Longitude
            );

            var result = await _sender.Send(command, cancellationToken);

            return Ok(new { Success = result, Message = "Successfully updated delivery trip." });
        }

        public record UpdateTripStatusRequest(
            TripStatus NewStatus,
            decimal Latitude,
            decimal Longitude
        );
    }
}
