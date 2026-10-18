using DeliveryApp.DeliveryApp.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Commands.UpdateOrderStatus
{
    public record UpdateOrderStatusCommand(
        Guid OrderId,
        OrderStatus NewStatus


        ): IRequest<bool>;
    
}
