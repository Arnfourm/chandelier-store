namespace microservices.CatalogAPI.API.Contracts.Requests
{
    public record ProductRequest
    (
        string Article,
        string Title,
        decimal Price,
        int Quantity,
        int? LampPower,
        int? LampCount,
        int ProductTypeId,
        IFormFile? Image
    );
}