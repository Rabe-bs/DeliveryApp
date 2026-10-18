using DeliveryApp.Application.Features.Auth.Common;
using MediatR;

namespace DeliveryApp.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<TokenResponseDto>;
}