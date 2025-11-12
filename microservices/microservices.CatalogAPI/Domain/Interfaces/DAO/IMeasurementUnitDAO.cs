using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IMeasurementUnitDAO
    {
        public Task<List<MeasurementUnit>> GetMeasurementUnits();
        public Task<MeasurementUnit> GetMeasurementUnitById(int id);
        public Task<IEnumerable<MeasurementUnit>> GetMeasurementUnitByIds(List<int> ids);
        public Task<MeasurementUnit> GetMeasurementUnitByTitle(string title);
        public Task CreateMeasurementUnit(MeasurementUnit measurementUnit);
        public Task UpdateMeasurementUnit(MeasurementUnit measurementUnit);
        public Task DeleteMeasurementUnitById(int id);
    }
}