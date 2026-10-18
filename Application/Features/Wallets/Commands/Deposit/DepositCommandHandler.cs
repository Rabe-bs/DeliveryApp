using DeliveryApp.Application.Interfaces;
using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace DeliveryApp.Application.Features.Wallets.Commands.Deposit
{
    public class DepositCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepositCommandHandler> _logger;

        public DepositCommandHandler(IUnitOfWork unitOfWork , ILogger<DepositCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
                
        }


        public async Task<bool> Handle(DepositCommand request , CancellationToken cancellationToken)
        {

            if (request.Amount <= 0)
            {
                _logger.LogWarning("Deposit failed: Invalid amount {Amount} for user ID: {UserId}", request.Amount, request.UserId);
                return false;
            }
            var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(request.UserId);

            if(wallet == null)
            {
            _logger.LogInformation("Creating new wallet for user Id : {UserId} during deposit.", request.UserId);

                wallet = new Wallet
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Balance = 0

                };
                await _unitOfWork.Wallets.AddAsync(wallet);
            }

            wallet.Balance += request.Amount;

            var transaction = new WalletTransaction
            {
                Id = Guid.NewGuid(),
                WalletId = wallet.Id,
                Amount = request.Amount,
                Type = TransactionType.Deposit,

                Description = request.Description ?? string.Empty,
                CreatedAt = DateTime.UtcNow


            };

            await _unitOfWork.WalletTransactions.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            return true;

        }
    }
}
