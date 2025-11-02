using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Infrastructure.Database.DAO
{
    public class AttributeDAO : IAttributeDAO
    {
        private readonly CatalogDbContext _catalogDbContext;

        public AttributeDAO(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<List<Attribute>> GetAttributes()
        {
            return await _catalogDbContext.Attributes
                .Select(attributeEntity => new Attribute
                (
                    attributeEntity.Id,
                    attributeEntity.Title,
                    attributeEntity.AttributeGroupId,
                    attributeEntity.MeasurementUnitId
                )).ToListAsync();
        }

        public async Task<Attribute> GetAttributeById(Guid id)
        {
            var attributeEntity = await _catalogDbContext.Attributes.FindAsync(id);

            if (attributeEntity == null)
            {
                throw new Exception($"Attribute with id {id} not found");
            }

            return new Attribute(attributeEntity.Id, attributeEntity.Title, attributeEntity.AttributeGroupId, attributeEntity.MeasurementUnitId);
        }

        public async Task<Guid> CreateAttribute(Attribute attribute)
        {
            var attributeEntity = new AttributeEntity
            {
                Title = attribute.GetTitle(),
                AttributeGroupId = attribute.GetAttributeGroupId(),
                MeasurementUnitId = attribute.GetMeasurementUnitId(),
            };

            await _catalogDbContext.Attributes.AddAsync(attributeEntity);
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new Attribute. Error message:\n{ex.Message}", ex);
            }

            return attributeEntity.Id;
        }

        public async Task DeleteAttributeById(Guid id)
        {
            await _catalogDbContext.Attributes.Where(attributeEntity => attributeEntity.Id == id).ExecuteDeleteAsync();
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to dekete Attribute. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
