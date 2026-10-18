using DeliveryApp.Application.Interfaces;
using MediatR;

namespace DeliveryApp.Application.Features.DeliveryTrips.Queries.GetDeliveryTripById
{
    public class GetDeliveryTripByIdQueryHandler : IRequestHandler<GetDeliveryTripByIdQuery, DeliveryTripDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeliveryTripByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeliveryTripDto?> Handle(GetDeliveryTripByIdQuery request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.DeliveryTrips.GetByIdAsync(request.TripId, cancellationToken);

            if (trip == null) return null;

            //  احول الكيان الى دي تي او 
            return new DeliveryTripDto(
                trip.Id,
                trip.OrderId,
                trip.DeliveryGuyId,
                trip.CurrentLatitude,
                trip.CurrentLongitude,
                trip.Status,
                trip.IsCompleted,
                trip.AssignedAt,
                trip.PickedUpAt,
                trip.DeliveredAt,
                trip.CancelledAt
            );
        }
    }
}