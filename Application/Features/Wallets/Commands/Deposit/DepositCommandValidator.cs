using FluentValidation;
namespace DeliveryApp.Application.Features.Wallets.Commands.Deposit
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {

        public DepositCommandValidator() 
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User Identity Requierd");
            RuleFor(x => x.Amount).GreaterThan(0)
                .WithMessage("Balence of Deposit shuld be greater than zero");

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage("Max Description is 250 litter");
        }

    }
}
