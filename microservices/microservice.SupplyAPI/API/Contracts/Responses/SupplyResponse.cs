namespace microservice.SupplyAPI.API.Contracts.Responses
{
    public record SupplyResponse
    (
        Guid Id,
        SupplierResponse Supplier,
        DateOnly SupplyDate,
        decimal TotalAmount
    );
}
