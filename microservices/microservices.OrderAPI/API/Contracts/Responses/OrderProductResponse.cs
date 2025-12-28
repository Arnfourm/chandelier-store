namespace microservices.OrderAPI.API.Contracts.Responses
{
    public record OrderProductResponse
    (
        Guid OrderId,
        Guid ProductId,
        decimal UnitPrice,
        int Quantity
    );
}