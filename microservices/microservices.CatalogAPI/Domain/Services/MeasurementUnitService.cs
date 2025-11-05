using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Services
{
    public class MeasurementUnitService : IMeasurementUnitService
    {
        private readonly IMeasurementUnitDAO _measurementUnitDAO;

        public MeasurementUnitService(IMeasurementUnitDAO measurementUnitDAO)
        {
            _measurementUnitDAO = measurementUnitDAO;
        }

        public async Task<List<MeasurementUnit>> GetAllMeasurementUnits()
        {
            List<MeasurementUnit> measurementUnits = await _measurementUnitDAO.GetMeasurementUnits();

            return measurementUnits;
        }

        public async Task<MeasurementUnit> GetSingleMeasurementUnitById(int id)
        {
            MeasurementUnit measurementUnit = await _measurementUnitDAO.GetMeasurementUnitById(id);

            return measurementUnit;
        }

        public async Task<int> CreateNewMeasurementUnit(MeasurementUnit measurementUnit)
        {
            int measurementUnitId = await _measurementUnitDAO.CreateMeasurementUnit(measurementUnit);

            return measurementUnitId;
        }

        public async Task DeleteSingleMeasurementUnitById(int id)
        {
            await _measurementUnitDAO.DeleteMeasurementUnitById(id);
        }
    }
}