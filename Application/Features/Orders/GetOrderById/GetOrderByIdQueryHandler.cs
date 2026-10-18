using Application.Interfaces;
using DeliveryApp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IAppDbContext _context;

        public GetOrderByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderDto = await _context.Orders
                .Where(o => o.Id == request.Id)
                .Select(o => new OrderDto(
                    o.Id,
                    o.CustomerId,
                    o.RestaurantId,
                    o.TotalAmount,
                    o.Status,
                    o.DeliveryGuyId,
                    o.DeliveryAddress,
                    o.CancellationReason,
                    o.IsRefunded,
                    o.CreatedAt
                ))
                .FirstOrDefaultAsync(cancellationToken);

            return orderDto;
        }
    }
}