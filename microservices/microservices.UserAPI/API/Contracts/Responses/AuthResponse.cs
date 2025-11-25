using microservices.UserAPI.Domain.Enums;

namespace microservices.UserAPI.API.Contracts.Responses
{
    public record AuthResponse
    (
        Guid UserId,
        string Email,
        UserRoleEnum UserRole,
        string AccessToken,
        string RefreshToken
    );
}
