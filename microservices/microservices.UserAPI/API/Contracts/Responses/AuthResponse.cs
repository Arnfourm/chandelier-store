namespace microservices.UserAPI.API.Contracts.Responses
{
    public record AuthResponse
    (
        Guid UserId,
        string Email,
        string AccessToken,
        string RefreshToken
    );
}
