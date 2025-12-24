namespace microservices.ReviewAPI.API.Contracts.Requests
{
    public record ReviewRequest
    (
        Guid UserId,
        Guid ProductId,
        Guid OrderId,
        int Rate,
        string? Content
    );
}
