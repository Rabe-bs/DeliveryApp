using FluentValidation;

namespace DeliveryApp.Application.Features.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("restaurent Name requierd.")
                .MaximumLength(150).WithMessage("Maximum linght is 150 litter");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Restaurent Adress requierd .")
                .MaximumLength(250).WithMessage("Aderess must be 150 litter ");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("phone number requierd .")
                .MaximumLength(20).WithMessage("incorect phone number ");
        }
    }
}