using MediatR;

namespace DeliveryApp.Application.Features.Users.Commands.ToggleUserStatus
{
    // لتفعيل او تعطيل المستخدم 
    public record ToggleUserStatusCommand(Guid Id) : IRequest<bool>;
}