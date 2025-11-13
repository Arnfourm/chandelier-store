namespace microservices.CatalogAPI.API.Contracts.Responses
{
    public record ProductAttributeResponse
    (
        AttributeResponse Attribute,
        string Value
    );
}
