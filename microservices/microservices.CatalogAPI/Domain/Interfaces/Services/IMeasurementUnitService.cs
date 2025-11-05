namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IMeasurementUnitService
    {
        public Task<List<MeasurementUnit>> GetAllMeasurementUnits();
        public Task<MeasurementUnit> GetSingleMeasurementUnitById(int id);
        public Task<int> CreateNewMeasurementUnit(MeasurementUnit measurementUnit);
        public Task DeleteSingleMeasurementUnitById(int id);
    }
}