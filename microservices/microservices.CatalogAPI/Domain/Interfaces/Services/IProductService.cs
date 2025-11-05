namespace microservices.CatalogAPI.DAO.Interfaces.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetSingleProductById(id);
        public Task<Guid> CreateNewProduct(Product product);
        public Task DeleteSingleProductById(id);
    }
}