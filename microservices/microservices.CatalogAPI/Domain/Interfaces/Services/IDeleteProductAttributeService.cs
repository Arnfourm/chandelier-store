namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IDeleteProductAttributeService
    {
        public Task DeleteListProductAttributesByProductId(Guid productId);
        public Task DeleteListProductAttributesByAttributeId(Guid attributeId);
        public Task DeleteSingleProductAttribute(Guid productId, Guid attributeId);
    }
}
