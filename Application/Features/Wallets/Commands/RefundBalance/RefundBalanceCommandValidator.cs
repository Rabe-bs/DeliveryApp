using FluentValidation;

namespace DeliveryApp.Application.Features.Wallets.Commands.RefundBalance
{
    public class RefundBalanceCommandValidator : AbstractValidator<RefundBalanceCommand>
    {
        public RefundBalanceCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User identity required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Refund amount must be greater than zero.");

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage("Description maximum length is 250 characters.");
        }
    }
}