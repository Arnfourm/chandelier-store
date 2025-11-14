using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IMeasurementUnitDAO
    {
        Task<List<MeasurementUnit>> GetMeasurementUnits();
        Task<MeasurementUnit> GetMeasurementUnitById(int id);
        Task<IEnumerable<MeasurementUnit>> GetMeasurementUnitByIds(List<int> ids);
        Task<MeasurementUnit> GetMeasurementUnitByTitle(string title);
        Task CreateMeasurementUnit(MeasurementUnit measurementUnit);
        Task UpdateMeasurementUnit(MeasurementUnit measurementUnit);
        Task DeleteMeasurementUnitById(int id);
    }
}