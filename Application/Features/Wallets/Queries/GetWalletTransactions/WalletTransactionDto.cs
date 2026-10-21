using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Features.Wallets.Queries.GetWalletTransactions
{
    public record WalletTransactionDto(
        Guid Id,
        decimal Amount,
        TransactionType Type,
        string Description,
        DateTime CreatedAt
    );
}