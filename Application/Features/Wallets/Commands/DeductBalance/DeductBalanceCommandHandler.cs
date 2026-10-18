using DeliveryApp.Application.Interfaces;
using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeliveryApp.Application.Features.Wallets.Commands.DeductBalance
{
    public class DeductBalanceCommandHandler : IRequestHandler<DeductBalanceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeductBalanceCommandHandler> _logger;

        public DeductBalanceCommandHandler(IUnitOfWork unitOfWork, ILogger<DeductBalanceCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeductBalanceCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(request.UserId);

            if (wallet == null || wallet.Balance < request.Amount)
            {
                _logger.LogWarning("Deduction failed for user ID: {UserId}. Insufficient balance or missing wallet. Requested: {Amount}, Available: {Balance}",
                    request.UserId, request.Amount, wallet?.Balance ?? 0);
                return false;
            }

            wallet.Balance -= request.Amount;

            var transaction = new WalletTransaction
            {
                Id = Guid.NewGuid(),
                WalletId = wallet.Id,
                Amount = request.Amount,
                Type = TransactionType.Deduction,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.WalletTransactions.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Successfully deducted {Amount} from wallet of user ID: {UserId}. Reason: {Description}",
                request.Amount, request.UserId, request.Description);

            return true;
        }
    }
}