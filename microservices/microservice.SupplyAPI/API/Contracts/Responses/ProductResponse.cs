namespace microservice.SupplyAPI.API.Contracts.Responses
{
    public record ProductResponse
    (
        Guid Id,
        string Article,
        string Title,
        decimal Price
    );
}
