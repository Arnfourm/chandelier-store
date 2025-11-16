using microservices.UserAPI.Domain.Enums;

namespace microservices.UserAPI.API.Contracts.Responses
{
    public record UserResponse
    (
        Guid Id,
        string Email,
        string Name,
        string Surname,
        DateOnly Birthday,
        DateTime Registration,
        Guid PasswordId,
        Guid RefreshTokenId,
        UserRoleEnum UserRole
    );
}