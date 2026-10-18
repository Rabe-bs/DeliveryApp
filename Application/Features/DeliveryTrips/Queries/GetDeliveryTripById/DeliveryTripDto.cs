using DeliveryApp.Domain.Enums;
using MediatR;

namespace DeliveryApp.Application.Features.DeliveryTrips.Queries.GetDeliveryTripById
{
    public record DeliveryTripDto(
        Guid Id,
        Guid OrderId,
        Guid DeliveryGuyId,
        decimal CurrentLatitude,
        decimal CurrentLongitude,
        TripStatus Status,
        bool IsCompleted,
        DateTime AssignedAt,
        DateTime? PickedUpAt,
        DateTime? DeliveredAt,
        DateTime? CancelledAt
    );

    public record GetDeliveryTripByIdQuery(Guid TripId) : IRequest<DeliveryTripDto?>;
}