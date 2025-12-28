namespace microservices.OrderAPI.Domain.DTO.Requests
{
    public class UserLoginRequestDTO
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}