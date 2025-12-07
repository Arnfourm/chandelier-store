namespace microservices.OrderAPI.API.Contracts.Responses
{
    public record OrderResponse
    (
        Guid Id,
        Guid UserId,
        decimal TotalAmount,
        StatusResponse Status,
        DeliveryTypeResponse DeliveryType,
        DateTime CreationDate
    );
}
