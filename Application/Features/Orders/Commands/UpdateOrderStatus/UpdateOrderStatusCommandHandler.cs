using DeliveryApp.Application.Features.Wallets.Commands.RefundBalance;
using DeliveryApp.Application.Interfaces;
using DeliveryApp.DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            // .  الطلب من قاعدة البيانات
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {request.OrderId} was not found.");
            }

            // التحقق من إمكانية تغيير الحالة
            if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.CancelledByCustomer)
            {
                throw new InvalidOperationException($"Cannot change status of an order that is already {order.Status}.");
            }

            //  معالجة الاسترجاع المالي عشان محد يقول انزرفت  Refund في حال الرفض أو الإلغاء أو الفشل
            if (request.NewStatus == OrderStatus.RejectedByRestaurant ||
                request.NewStatus == OrderStatus.CancelledByCustomer ||
                request.NewStatus == OrderStatus.FailedDueToError)
            {
                var refundCommand = new RefundBalanceCommand(
                    order.CustomerId,
                    order.TotalAmount,
                    $"Refund for Order #{order.Id} status changed to {request.NewStatus}"
                );

                await _mediator.Send(refundCommand, cancellationToken);
            }

            order.UpdateStatus(request.NewStatus);

            await _unitOfWork.Orders.Update(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}