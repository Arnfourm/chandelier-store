namespace microservice.SupplyAPI.API.Contracts.Requests
{
    public record SupplierRequest
    (
        string Name,
        int DeliveryTypeId
    );
}
