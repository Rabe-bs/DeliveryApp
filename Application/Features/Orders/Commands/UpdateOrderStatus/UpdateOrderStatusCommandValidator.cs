using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("The Order Id Is Required");

            RuleFor(x => x.NewStatus).IsInEnum()
                .WithMessage("Invalid Order Status Provide.");
        }

    }
}
