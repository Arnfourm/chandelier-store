using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IMeasurementUnitService
    {
        public Task<IEnumerable<MeasurementUnitResponse>> GetAllMeasurementUnits();
        public Task<MeasurementUnit> GetSingleMeasurementUnitById(int id);
        Task<IEnumerable<MeasurementUnit>> GetListMeasurementUnitByIds(List<int> ids);
        Task<MeasurementUnit> GetSingleMeasurementUnitByTitle(string title);
        public Task CreateNewMeasurementUnit(MeasurementUnitRequest request);
        public Task UpdateSingleMeasurementUnit(int id, MeasurementUnitRequest request);
        public Task DeleteSingleMeasurementUnitById(int id);
    }
}