using DeliveryApp.Application.Interfaces;
using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeliveryApp.Application.Features.Wallets.Commands.RefundBalance
{
    public class RefundBalanceCommandHandler : IRequestHandler<RefundBalanceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RefundBalanceCommandHandler> _logger;

        public RefundBalanceCommandHandler(IUnitOfWork unitOfWork, ILogger<RefundBalanceCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(RefundBalanceCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(request.UserId);

            if (wallet == null)
            {
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
                Type = TransactionType.Refund,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.WalletTransactions.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Successfully refunded {Amount} to wallet of user ID: {UserId}. Reason: {Description}",
                request.Amount, request.UserId, request.Description);

            return true;
        }
    }
}