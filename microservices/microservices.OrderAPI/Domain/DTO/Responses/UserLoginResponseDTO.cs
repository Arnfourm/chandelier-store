namespace microservices.OrderAPI.Domain.DTO.Responses
{
    public class UserLoginResponseDTO
    {
        public Guid userId { get; set; }
        public required string email { get; set; }
        public int userRole { get; set; }
        public required string accessToken { get; set; }
        public required string refreshToken { get; set; }
    }
}