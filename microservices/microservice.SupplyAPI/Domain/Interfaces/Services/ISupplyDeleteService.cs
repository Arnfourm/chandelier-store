namespace microservice.SupplyAPI.Domain.Interfaces.Services
{
    public interface ISupplyDeleteService
    {
        Task DeleteSingleSupplyById(Guid id);
    }
}
