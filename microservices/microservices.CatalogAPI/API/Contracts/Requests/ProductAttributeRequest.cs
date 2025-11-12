namespace microservices.CatalogAPI.API.Contracts.Requests
{
    public record ProductAttributeRequest
    (
        Guid ProductId,
        Guid AttributeId,
        string Value
    );
}
