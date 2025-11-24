namespace microservice.SupplyAPI.API.Contracts.Responses
{
    public record SupplyProductResponse
    (
        ProductResponse Product,
        int Quantity
    );
}