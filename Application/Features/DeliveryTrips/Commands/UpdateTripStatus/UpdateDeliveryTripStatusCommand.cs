using DeliveryApp.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.DeliveryTrips.Commands.UpdateTripStatus
{
    public record UpdateDeliveryTripStatusCommand
    (
        Guid TripId,
        TripStatus NewStatus,
        decimal Latitude,
        decimal Longitude

        ) : IRequest<bool>;
}
