using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
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

        public async Task<IEnumerable<MeasurementUnitResponse>> GetAllMeasurementUnits()
        {
            List<MeasurementUnit> measurementUnits = await _measurementUnitDAO.GetMeasurementUnits();

            IEnumerable<MeasurementUnitResponse> response = measurementUnits
                .Select(measurementUnits =>
                    new MeasurementUnitResponse
                    (
                        measurementUnits.GetId(),
                        measurementUnits.GetTitle()
                    )
                );

            return response;
        }

        public async Task<MeasurementUnit> GetSingleMeasurementUnitById(int id)
        {
            MeasurementUnit measurementUnit = await _measurementUnitDAO.GetMeasurementUnitById(id);

            return measurementUnit;
        }

        public async Task<IEnumerable<MeasurementUnit>> GetListMeasurementUnitByIds(List<int> ids)
        {
            IEnumerable<MeasurementUnit> measurementUnits = await _measurementUnitDAO.GetMeasurementUnitByIds(ids);

            return measurementUnits;
        }

        public async Task<MeasurementUnit> GetSingleMeasurementUnitByTitle(string title)
        {
            MeasurementUnit measurementUnit = await _measurementUnitDAO.GetMeasurementUnitByTitle(title);

            return measurementUnit;
        }

        public async Task CreateNewMeasurementUnit(MeasurementUnitRequest request)
        {
            MeasurementUnit newMeasurementUnit = new MeasurementUnit(request.Title);

            await _measurementUnitDAO.CreateMeasurementUnit(newMeasurementUnit);
        }

        public async Task UpdateSingleMeasurementUnit(int id, MeasurementUnitRequest request)
        {
            MeasurementUnit updateMeasurementUnit = new MeasurementUnit(id, request.Title);

            await _measurementUnitDAO.UpdateMeasurementUnit(updateMeasurementUnit);
        }

        public async Task DeleteSingleMeasurementUnitById(int id)
        {
            await _measurementUnitDAO.DeleteMeasurementUnitById(id);
        }
    }
}