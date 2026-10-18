using FluentValidation;

namespace DeliveryApp.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Customer ID is required.");

            RuleFor(x => x.RestaurantId)
                .NotEmpty()
                .WithMessage("Restaurant ID is required.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("Total amount must be greater than zero.");

            RuleFor(x => x.DeliveryAddress)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Delivery address is required and cannot exceed 500 characters.");
        }
    }
}