using microservices.UserAPI.Domain.Enums;

namespace microservices.UserAPI.API.Contracts.Responses
{
    public record UserResponse
    (
        string Email,
        string Name,
        string Surname,
        DateOnly? Birthday,
        DateTime Registration
    );
}