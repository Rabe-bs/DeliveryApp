using DeliveryApp.Application.Interfaces;
using DeliveryApp.Application.Features.DeliveryTrips.Queries.GetDeliveryTripById;
using MediatR;

namespace DeliveryApp.Application.Features.DeliveryTrips.Queries.GetActiveTripsByDeliveryGuy
{
    public class GetActiveTripsByDeliveryGuyQueryHandler : IRequestHandler<GetActiveTripsByDeliveryGuyQuery, IEnumerable<DeliveryTripDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetActiveTripsByDeliveryGuyQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DeliveryTripDto>> Handle(GetActiveTripsByDeliveryGuyQuery request, CancellationToken cancellationToken)
        {
            // gg جلب الرحلات باستخدام دالة ممكن تتعدل اكثر بطريقه احسن بس مافكرت فيها 
            var allTrips = await _unitOfWork.DeliveryTrips.GetAllAsync(cancellationToken);

            var activeTrips = allTrips
                .Where(t => t.DeliveryGuyId == request.DeliveryGuyId && !t.IsCompleted);

            // تحويل الكائنات إلى DTOs
            return activeTrips.Select(trip => new DeliveryTripDto(
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
            ));
        }
    }
}