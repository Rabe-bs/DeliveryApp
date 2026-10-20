using MediatR;

namespace DeliveryApp.Application.Features.Users.Queries.GetUsersList
{
    public record GetUsersListQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<UserListDto>>;
}