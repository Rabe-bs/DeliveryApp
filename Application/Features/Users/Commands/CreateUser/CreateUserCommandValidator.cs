using FluentValidation;

namespace DeliveryApp.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is requierd.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is requierd")
                .EmailAddress().WithMessage("incorrect Email ");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("phone number requierd.")
                .MaximumLength(20);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password id requierd")
                .MinimumLength(6).WithMessage("Password must be minimum 6 litter");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("incorect rule ");
        }
    }
}