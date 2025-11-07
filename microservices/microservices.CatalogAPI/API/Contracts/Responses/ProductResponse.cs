namespace microservices.CatalogAPI.API.Contracts.Responses
{
    public record ProductResponse
    (
        Guid Id,
        string Article,
        string Title,
        decimal Price,
        int Quantity,
        string ProductType,
        DateOnly AddedDate
    );
}