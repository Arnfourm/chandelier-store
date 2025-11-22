namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IPasswordService
    {
        (byte[] Hash, byte[] Salt) HashPassword(string password);
    }
}
