namespace microservices.OrderAPI.API.Contracts.Requests
{
    
    public record OrderRequest
    (
        // Take user id from cookie in future
        Guid UserId,
        decimal TotalAmount,
        int StatusId,
        int DeliveryTypeId
    );
}
