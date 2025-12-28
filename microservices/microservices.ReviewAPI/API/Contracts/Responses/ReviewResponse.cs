namespace microservices.ReviewAPI.API.Contracts.Responses
{
    public record ReviewResponse
    (
        Guid Id,
        int Rate,
        string? Content,
        DateTime CreationDate
    );
}
