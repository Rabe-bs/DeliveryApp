using FluentValidation;

namespace DeliveryApp.Application.Features.Users.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("identity user is requierd.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is requierd.")
                .MaximumLength(100).WithMessage("max lenght in name is 100 litter.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("phone numper is requierdب.")
                .MaximumLength(20).WithMessage("phone number expierd.");
        }
    }
}