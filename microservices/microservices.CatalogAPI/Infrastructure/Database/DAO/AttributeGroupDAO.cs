using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.Infrastructure.Database.Contexts;
using microservices.CatalogAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.CatalogAPI.Infrastructure.Database.DAO
{
    public class AttributeGroupDAO : IAttributeGroupDAO
    {
        private readonly CatalogDbContext _catalogDbContext;

        public AttributeGroupDAO(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task<List<AttributeGroup>> GetAttributeGroups()
        {
            return await _catalogDbContext.AttributeGroups
                .Select(attributeGroupEntity => new AttributeGroup
                (
                    attributeGroupEntity.Id,
                    attributeGroupEntity.Title
                )).ToListAsync();
        }

        public async Task<AttributeGroup> GetAttributeGroupById(int id)
        {
            var attributeGroupEntity = await _catalogDbContext.AttributeGroups.FindAsync(id);

            if (attributeGroupEntity == null)
            {
                throw new Exception($"Attribute group with id {id} not found");
            }

            return new AttributeGroup(attributeGroupEntity.Id, attributeGroupEntity.Title);
        }

        public async Task<int> CreateAttributeGroups(AttributeGroup attributeGroup)
        {
            var attributeGroupEntity = new AttributeGroupEntity
            {
                Title = attributeGroup.GetTitle()
            };

            await _catalogDbContext.AttributeGroups.AddAsync(attributeGroupEntity);
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new Attribute Group. Error message:\n{ex.Message}", ex);
            }

            return attributeGroupEntity.Id;
        }

        public async Task<int> UpdateAttributeGroup(AttributeGroup attributeGroup)
        {
            await _catalogDbContext.AttributeGroups
                .Where(attributeGroupEntity => attributeGroupEntity.Id == attributeGroup.GetId())
                .ExecuteUpdateAsync(attributeGroupSetters => attributeGroupSetters
                    .SetProperty(attributeGroupEntity => attributeGroupEntity.Title, attributeGroup.GetTitle()));

            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"Error while trying to update attribute group. Error message:\n{ex.Message}", ex);
            }

            return attributeGroup.GetId();
        }

        public async Task DeleteAttributeGroupById(int id)
        {
            await _catalogDbContext.AttributeGroups
                .Where(attributeGroupEntity => attributeGroupEntity.Id == id)
                .ExecuteDeleteAsync();
            try
            {
                await _catalogDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete Attribute Group. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
