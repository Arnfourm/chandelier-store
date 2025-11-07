namespace microservices.CatalogAPI.API.Contracts.Requests
{
    public record ProductRequest
    (
        string Article,
        string Title,
        decimal Price,
        int Quantity,
        String ProductType,
        DateOnly AddedDate
    );
}