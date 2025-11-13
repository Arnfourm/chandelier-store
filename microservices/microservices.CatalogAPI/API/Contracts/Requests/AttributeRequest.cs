namespace microservices.CatalogAPI.API.Contracts.Requests
{
    public record AttributeRequest
    (
        string Title,
        int AttributeGroupId,
        int MeasurementUnitId
    );
}
