using DeliveryApp.DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Enums;
using MediatR;

namespace DeliveryApp.Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string FullName,
        string Email,
        string PhoneNumber,
        string Password,
        UserRole Role
    ) : IRequest<Guid>;
}