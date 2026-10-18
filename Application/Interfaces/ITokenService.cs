using DeliveryApp.DeliveryApp.Domain.Entities;

namespace DeliveryApp.Application.Interfaces
{
    using DeliveryApp.Domain.Entities;
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}
