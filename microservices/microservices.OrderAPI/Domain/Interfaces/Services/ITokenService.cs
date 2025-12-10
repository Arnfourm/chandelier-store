namespace microservices.OrderAPI.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
    }
}