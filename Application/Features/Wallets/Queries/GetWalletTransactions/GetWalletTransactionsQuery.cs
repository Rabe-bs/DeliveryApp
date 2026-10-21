using MediatR;

namespace DeliveryApp.Application.Features.Wallets.Queries.GetWalletTransactions
{
    public record GetWalletTransactionsQuery(Guid UserId) : IRequest<List<WalletTransactionDto>>;
}