using DeliveryApp.Application.Interfaces;
using DeliveryApp.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.DeliveryTrips.Commands.UpdateTripStatus
{
    internal class UpdateDeliveryTripStatusCommandHandler : IRequestHandler<UpdateDeliveryTripStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDeliveryTripStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateDeliveryTripStatusCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.DeliveryTrips.GetByIdAsync(request.TripId, cancellationToken);
           
            if(trip == null)
            {
                throw new KeyNotFoundException($"Delivery Trip With Id { request.TripId} Was not Fund");


            }

            switch (request.NewStatus)
            {
                case TripStatus.PickedUp:
                    trip.MarkAsPickedUp(request.Latitude, request.Longitude);
                    break;
                case TripStatus.Completed:
                    trip.CompleteTrip(request.Latitude, request.Longitude);
                    break;
                case TripStatus.Cancelled:
                    trip.CancelTrip();
                    break;
                default:
                    throw new ArgumentException("Invallid Trip Status Transition");

            }


            await _unitOfWork.DeliveryTrips.Update(trip);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
