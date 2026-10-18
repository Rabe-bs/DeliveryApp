using DeliveryApp.Domain.Entities;
using System.Runtime.CompilerServices;

namespace DeliveryApp.Application.Features.Wallets.Queries.GetBalance
{
    public static class BalanceMappingExtensions
    {

        public static WalletBalanceResponseDto ToBalanceResponse(this Wallet? wallet , string message)
        {
            return new WalletBalanceResponseDto
            {
                Balance = wallet?.Balance ?? 0,
                Message = message

            };

        }
    }
}
