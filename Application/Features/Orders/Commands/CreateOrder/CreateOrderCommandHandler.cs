using DeliveryApp.Application.Features.Wallets.Commands.DeductBalance;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeliveryApp.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IMediator mediator,
            ILogger<CreateOrderCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
            {
                throw new InvalidOperationException("Order must contain at least one item.");
            }

            var order = new Order(
                request.CustomerId,
                request.RestaurantId,
                request.DeliveryAddress
            );

            foreach (var item in request.Items)
            {
                order.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            }

            if (order.TotalAmount <= 0)
            {
                throw new InvalidOperationException("Order total amount must be greater than zero.");
            }

            var deductionCommand = new DeductBalanceCommand(
                request.CustomerId,
                order.TotalAmount,
                $"Payment for Order to Restaurant: {request.RestaurantId}"
            );

            var isDeducted = await _mediator.Send(deductionCommand, cancellationToken);

            if (!isDeducted)
            {
                _logger.LogWarning("Order creation failed for customer ID: {CustomerId} due to insufficient wallet balance.", request.CustomerId);
                throw new InvalidOperationException("Insufficient wallet balance to complete the order.");
            }

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Order ID: {OrderId} successfully created for customer ID: {CustomerId}", order.Id, request.CustomerId);

            return order.Id;
        }
    }
}