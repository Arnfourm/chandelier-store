namespace microservices.OrderAPI.API.Contracts.Requests
{
    public record OrderProductRequest
    (
        Guid OrderId,
        Guid ProductId,
        int Quantity
    );
}
