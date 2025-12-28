namespace microservices.OrderAPI.Domain.DTO.Responses
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateOnly? Birthday { get; set; }
        public DateTime Registration {  get; set; }
        public int UserRole { get; set; }
    }
}
