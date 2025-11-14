namespace microservice.SupplyAPI.API.Contracts.Responses
{
    public record SupplierResponse
    (
        Guid Id,
        string Name,
        DeliveryTypeResponse DeliveryType
    );
}
