namespace microservice.SupplyAPI.API.Contracts.Requests
{
    public record SupplyRequest
    (
        Guid SupplierId,
        decimal TotalAmount
    );
}
