using MediatR;
namespace DeliveryApp.Application.Features.Wallets.Commands.Deposit
{
    public record DepositCommand(Guid UserId, decimal Amount , string? Description = null) : IRequest<bool>;
    
}
