using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Domain.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeDAO _attributeDAO;

        public AttributeService(IAttributeDAO attributeDAO)
        {
            _attributeDAO = attributeDAO;
        }
        
        public async Task<List<Attribute>> GetAllAttributes()
        {
            List<Attribute> attributes = await _attributeDAO.GetAttributes();

            return attributes;
        }

        public async Task<Attribute> GetSingleAttributeById(Guid id)
        {
            Attribute attribute = await _attributeDAO.GetAttributeById(id);

            return attribute;
        }

        public async Task<Guid> CreateNewAttribute(Attribute attribute)
        {
            Guid attributeId = await _attributeDAO.CreateAttribute(attribute);

            return attributeId;
        }

        public async Task DeleteSingleAttributeById(Guid id)
        {
            await _attributeDAO.DeleteAttributeById(id);
        }
    }
}