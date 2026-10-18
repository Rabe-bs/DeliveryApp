using MediatR;

namespace DeliveryApp.Application.Features.Wallets.Commands.RefundBalance
{
    public record RefundBalanceCommand(Guid UserId, decimal Amount, string Description) : IRequest<bool>;
}