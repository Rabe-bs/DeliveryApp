using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeliveryApp.Application.Features.Wallets.Queries.GetBalance
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, WalletBalanceResponseDto>
    {
        private readonly ILogger<GetBalanceQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetBalanceQueryHandler(IUnitOfWork unitOfWork, ILogger<GetBalanceQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<WalletBalanceResponseDto> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(request.UserId);

            if (wallet == null)
            {
                _logger.LogInformation("Wallet not found for user ID: {UserId} during balance check. Returning 0.", request.UserId);
                //   Extension   
                return wallet.ToBalanceResponse("Wallet not found, balance is zero.");
            }

            //   Extension 
            return wallet.ToBalanceResponse("Balance retrieved successfully.");
        }
    }
}