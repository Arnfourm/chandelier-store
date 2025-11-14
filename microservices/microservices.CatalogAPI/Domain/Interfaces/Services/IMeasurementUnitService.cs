using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IMeasurementUnitService
    {
        Task<IEnumerable<MeasurementUnitResponse>> GetAllMeasurementUnits();
        Task<MeasurementUnit> GetSingleMeasurementUnitById(int id);
        Task<IEnumerable<MeasurementUnitResponse>> GetListMeasurementUnitResponseByIds(List<int> ids);
        Task<MeasurementUnit> GetSingleMeasurementUnitByTitle(string title);
        Task CreateNewMeasurementUnit(MeasurementUnitRequest request);
        Task UpdateSingleMeasurementUnit(int id, MeasurementUnitRequest request);
        Task DeleteSingleMeasurementUnitById(int id);
    }
}