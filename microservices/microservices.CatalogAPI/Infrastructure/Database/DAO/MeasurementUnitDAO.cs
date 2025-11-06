using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.CatalogAPI.Infrastructure.Database.DAO
{
    public class MeasurementUnitDAO : IMeasurementUnitDAO
    {
        private readonly CatalogDbContext _catalogDbContext;

        public MeasurementUnitDAO(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<List<MeasurementUnit>> GetMeasurementUnits()
        {
            return await _catalogDbContext.MeasurementUnits
                .Select(measurementUnitEntity => new MeasurementUnit
                (
                    measurementUnitEntity.Id,
                    measurementUnitEntity.Title
                )).ToListAsync();
        }

        public async Task<MeasurementUnit> GetMeasurementUnitById(int id)
        {
            var measurementUnitEntity = await _catalogDbContext.MeasurementUnits.FindAsync(id);

            if (measurementUnitEntity == null)
            {
                throw new Exception($"Measurement unit with id {id} not found");
            }

            return new MeasurementUnit(measurementUnitEntity.Id, measurementUnitEntity.Title);
        }

        public async Task<int> CreateMeasurementUnit(MeasurementUnit measurementUnit)
        {
            var measurementUnitEntity = new MeasurementUnitEntity
            {
                Title = measurementUnit.GetTitle(),
            };

            await _catalogDbContext.MeasurementUnits.AddAsync(measurementUnitEntity);
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new measurement unit. Error message:\n{ex.Message}", ex);
            }

            return measurementUnitEntity.Id;
        }

        public async Task<int> UpdateMeasurementUnit(MeasurementUnit measurementUnit)
        {
            await _catalogDbContext.MeasurementUnits
                .Where(measurementUnitEntity => measurementUnitEntity.Id == measurementUnit.GetId())
                .ExecuteUpdateAsync(measurementUnitSetters => measurementUnitSetters
                    .SetProperty(measurementUnitEntity => measurementUnitEntity.Title, measurementUnit.GetTitle()));

            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"Error while trying to update measurement unit. Error message:\n{ex.Message}", ex);
            }

            return measurementUnit.GetId();
        }

        public async Task DeleteMeasurementUnitById(int id)
        {
            await _catalogDbContext.MeasurementUnits
                .Where(measurementUnitEntity => measurementUnitEntity.Id == id)
                .ExecuteDeleteAsync();
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete measurement unit. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
