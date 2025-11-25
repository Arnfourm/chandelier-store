namespace microservices.UserAPI.API.Contracts.Requests
{
    public record LoginRequest
    (
        string Email,
        string Password
    );
}
