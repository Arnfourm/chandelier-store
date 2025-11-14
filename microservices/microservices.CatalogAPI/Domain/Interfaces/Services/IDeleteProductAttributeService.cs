namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IDeleteProductAttributeService
    {
        Task DeleteListProductAttributesByProductId(Guid productId);
        Task DeleteListProductAttributesByAttributeId(Guid attributeId);
        Task DeleteSingleProductAttribute(Guid productId, Guid attributeId);
    }
}
