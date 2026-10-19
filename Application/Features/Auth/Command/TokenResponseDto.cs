namespace DeliveryApp.Application.Features.Auth.Common
{
    public class TokenResponseDto
    {
        //  هذا يستخدمه الكلايم 
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}