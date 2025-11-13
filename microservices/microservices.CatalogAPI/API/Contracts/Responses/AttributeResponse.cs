namespace microservices.CatalogAPI.API.Contracts.Responses
{
    public record AttributeResponse
    (
        Guid id,
        string title,
        AttributeGroupResponse attributeGroup,
        MeasurementUnitResponse measurementUnit
    );
}
