using MediatR;

namespace DeliveryApp.Application.Features.Wallets.Commands.DeductBalance
{
    public record DeductBalanceCommand(Guid UserId, decimal Amount, string Description) : IRequest<bool>;
}