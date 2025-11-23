using microservices.UserAPI.Domain.Enums;

namespace microservices.UserAPI.API.Contracts.Requests
{
    public record UserRequest
    (
        string Email,
        string Name,
        string Surname,
        string? Password,
        DateOnly? Birthday,
        UserRoleEnum UserRole = UserRoleEnum.Client,

        Guid? PasswordId = null,
        Guid? RefreshTokenId = null
    );
}