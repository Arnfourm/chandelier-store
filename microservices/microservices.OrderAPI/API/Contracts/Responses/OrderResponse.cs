namespace microservices.OrderAPI.API.Contracts.Responses
{
    public record OrderResponse
    (
        Guid Id,
        decimal TotalAmount,
        StatusResponse Status,
        DeliveryTypeResponse DeliveryType,
        DateTime CreationDate
    );
}
