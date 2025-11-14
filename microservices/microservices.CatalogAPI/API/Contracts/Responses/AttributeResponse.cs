namespace microservices.CatalogAPI.API.Contracts.Responses
{
    public record AttributeResponse
    (
        Guid Id,
        string Title,
        AttributeGroupResponse AttributeGroup,
        MeasurementUnitResponse MeasurementUnit
    );
}
