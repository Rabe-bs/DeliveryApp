using MediatR;

namespace DeliveryApp.Application.Features.Wallets.Queries.GetBalance;

public record GetBalanceQuery(Guid UserId) : IRequest<WalletBalanceResponseDto>;