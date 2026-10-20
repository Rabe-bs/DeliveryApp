using DeliveryApp.DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Features.Users.Queries.GetUserById
{
    public record UserDto(
        Guid Id,
        string FullName,
        string Email,
        string PhoneNumber,
        UserRole Role,
        bool IsActive
    );
}