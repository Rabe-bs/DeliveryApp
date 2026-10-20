using MediatR;

namespace DeliveryApp.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
}