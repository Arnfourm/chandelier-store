using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IMeasurementUnitDAO
    {
        public Task<List<MeasurementUnit>> GetMeasurementUnits();
        public Task<MeasurementUnit> GetMeasurementUnitById(int id);
        public Task<int> CreateMeasurementUnit(MeasurementUnit measurementUnit);
        public Task<int> UpdateMeasurementUnit(MeasurementUnit measurementUnit);
        public Task DeleteMeasurementUnitById(int id);
    }
}