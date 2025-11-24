namespace microservice.SupplyAPI.API.Contracts.Requests
{
    public record SupplyProductRequest
    (
        Guid SupplyId,
        Guid ProductId,
        int Quantity
    );
}
