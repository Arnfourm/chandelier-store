using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;

namespace microservices.CatalogAPI.Domain.Services
{
    public class DeleteProductAttributeService : IDeleteProductAttributeService
    {
        private readonly IProductAttributeDAO _productAttributeDAO;

        public DeleteProductAttributeService(IProductAttributeDAO productAttributeDAO)
        {
            _productAttributeDAO = productAttributeDAO;
        }

        public async Task DeleteListProductAttributesByProductId(Guid productId)
        {
            await _productAttributeDAO.DeleteProductAttributeByProductId(productId);
        }

        public async Task DeleteListProductAttributesByAttributeId(Guid attributeId)
        {
            await _productAttributeDAO.DeleteProductAttributeByAttributeId(attributeId);
        }

        public async Task DeleteSingleProductAttribute(Guid productId, Guid attributeId)
        {
            await _productAttributeDAO.DeleteProductAttributeByBothIds(productId, attributeId);
        }
    }
}
