using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Features.DeliveryTrips.Commands.UpdateTripStatus
{
    public class UpdateDeliveryTripStatusCommandValidator : AbstractValidator<UpdateDeliveryTripStatusCommand>
    {
        public UpdateDeliveryTripStatusCommandValidator()
        {
            RuleFor(x => x.TripId)
                .NotEmpty().WithMessage("Identity Is Required ");

            RuleFor(x => x.NewStatus)
                .IsInEnum()
                .WithMessage("Status Delivery Not Clear");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("The Latitude Must be btwen -90 And 90");
            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("The Latitude Must be btwen -90 And 90");
        }
    }
}
