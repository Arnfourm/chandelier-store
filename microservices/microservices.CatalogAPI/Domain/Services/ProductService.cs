namespace namespace.CatalogAPI.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDAO _productDAO;

        public ProductService(IProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = await _productDAO.GetProducts();

            return products;
        }

        public async Task<Product> GetSingleProductById(Guid id)
        {
            Product product = await _productDAO.GetProductById(id);

            return product;
        }

        public async Task<Guid> CreateNewProduct(Product product)
        {
            Guid productId = await _productDAO.CreateProduct(product);

            return productId;
        }

        public async Task DeleteSingleProductById(Guid id)
        {
            await _productDAO.DeleteProductById(id);
        }
    }
}