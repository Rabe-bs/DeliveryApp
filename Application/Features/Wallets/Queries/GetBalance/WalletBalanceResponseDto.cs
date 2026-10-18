namespace DeliveryApp.Application.Features.Wallets.Queries.GetBalance
{
    public class WalletBalanceResponseDto
    {
        public decimal Balance { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
