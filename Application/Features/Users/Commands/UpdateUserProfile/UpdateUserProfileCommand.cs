using MediatR;

namespace DeliveryApp.Application.Features.Users.Commands.UpdateUserProfile
{
    public record UpdateUserProfileCommand(
        Guid Id,
        string FullName,
        string PhoneNumber
    ) : IRequest<bool>;
}