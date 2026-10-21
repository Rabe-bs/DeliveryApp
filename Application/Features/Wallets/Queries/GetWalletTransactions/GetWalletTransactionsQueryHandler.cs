using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Wallets.Queries.GetWalletTransactions
{
    public class GetWalletTransactionsQueryHandler : IRequestHandler<GetWalletTransactionsQuery, List<WalletTransactionDto>>
    {
        private readonly IAppDbContext _context;

        public GetWalletTransactionsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WalletTransactionDto>> Handle(GetWalletTransactionsQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets
                .AsNoTracking()
                .Include(w => w.WalletTransactions)
                .FirstOrDefaultAsync(w => w.UserId == request.UserId, cancellationToken);

            if (wallet == null)
            {
                return new List<WalletTransactionDto>();
            }

            // جلب الحركات مرتبة من الأحدث للأقدم
            return wallet.WalletTransactions
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new WalletTransactionDto(
                    t.Id,
                    t.Amount,
                    t.Type,
                    t.Description,
                    t.CreatedAt
                ))
                .ToList();
        }
    }
}