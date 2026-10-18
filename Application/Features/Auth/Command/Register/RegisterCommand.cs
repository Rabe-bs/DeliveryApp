using DeliveryApp.Application.Features.Auth.Common;
using DeliveryApp.DeliveryApp.Domain.Enums;
using MediatR;

namespace DeliveryApp.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
        string FullName,
        string Email,
        string Password,
        UserRole Role
    ) : IRequest<TokenResponseDto>;
}