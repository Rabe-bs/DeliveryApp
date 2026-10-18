using DeliveryApp.Application.Features.DeliveryTrips.Queries.GetDeliveryTripById;
using MediatR;

namespace DeliveryApp.Application.Features.DeliveryTrips.Queries.GetActiveTripsByDeliveryGuy
{
    public record GetActiveTripsByDeliveryGuyQuery(Guid DeliveryGuyId) : IRequest<IEnumerable<DeliveryTripDto>>;
}