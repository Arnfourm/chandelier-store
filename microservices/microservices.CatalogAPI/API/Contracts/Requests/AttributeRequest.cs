namespace microservices.CatalogAPI.API.Contracts.Requests
{
    public record AttributeRequest
    (
        string Title,
        string AttributeGroup,
        string MeasurementUnit
    );
}
