namespace microservices.ReviewAPI.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
    }
}