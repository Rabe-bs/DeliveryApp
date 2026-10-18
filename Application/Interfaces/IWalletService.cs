namespace DeliveryApp.Application.Interfaces
{
    public interface IWalletService
    {
        Task<bool> DeductBalanceAsync(Guid userId, decimal amount, string description);
        Task<bool> RefundBalanceAsync(Guid userId, decimal amount, string description);
        Task<decimal> GetBalanceAsync(Guid userId);
        Task<bool> DepositAsync(Guid userId, decimal amount, string description); // للإيداع وشحن المحفظة


    }
}
