using FluentValidation;

namespace DeliveryApp.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("identity is requierd .");
        }
    }
}