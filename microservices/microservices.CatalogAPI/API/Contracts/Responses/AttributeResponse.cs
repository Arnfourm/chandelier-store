namespace microservices.CatalogAPI.API.Contracts.Responses
{
    public record AttributeResponse
    (
        Guid id,
        string title,
        string attributeGroup,
        string measurementUnit
    );
}
