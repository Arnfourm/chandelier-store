namespace microservices.OrderAPI.API.Contracts.Requests
{
    
    public record OrderRequest
    (
        // Take user id from cookie
        decimal TotalAmount,
        int StatusId,
        int DeliveryTypeId
    );
}
