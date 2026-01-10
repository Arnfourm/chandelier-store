using Moq;
using microservices.CatalogAPI.Domain.Services;
using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;
using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace TestMicroservices
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductDAO> _mockProductDAO;
        private Mock<IProductTypeService> _mockProductTypeService;
        private Mock<IDeleteProductAttributeService> _mockDeleteProductAttributeService;
        private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private ProductService _productService;

        private Product _testProduct;
        private ProductType _testProductType;
        private ProductTypeResponse _testProductTypeResponse;
        private ProductRequest _validProductRequest;
        private ProductRequest _invalidProductRequest;

        [SetUp]
        public void Setup()
        {
            _mockProductDAO = new Mock<IProductDAO>();
            _mockProductTypeService = new Mock<IProductTypeService>();
            _mockDeleteProductAttributeService = new Mock<IDeleteProductAttributeService>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();

            _productService = new ProductService(
                _mockProductDAO.Object,
                _mockProductTypeService.Object,
                _mockDeleteProductAttributeService.Object,
                _mockWebHostEnvironment.Object
            );

            var productId = Guid.NewGuid();
            _testProduct = new Product(
                productId,
                "TEST001",
                "Test Product",
                100.0m,
                10,
                60,
                4,
                1,
                "/images/test.jpg",
                DateOnly.FromDateTime(DateTime.Now)
            );

            _testProductType = new ProductType(1, "Lamp");
            _testProductTypeResponse = new ProductTypeResponse(1, "Lamp");

            var mockImage = new Mock<IFormFile>();
            _validProductRequest = new ProductRequest(
                "TEST001",
                "Test Product",
                100.0m,
                10,
                60,
                4,
                1,
                mockImage.Object
            );

            _invalidProductRequest = new ProductRequest(
                "",
                "",
                -100.0m,
                -10,
                -60,
                -4,
                0,
                null
            );
        }

        [Test]
        public async Task GetAllProducts()
        {
            var products = new List<Product>
            {
                _testProduct,
                new Product(
                    Guid.NewGuid(),
                    "TEST002",
                    "Test Product 2",
                    200.0m,
                    20,
                    80,
                    6,
                    2,
                    "/images/test2.jpg",
                    DateOnly.FromDateTime(DateTime.Now)
                )
            };

            var productTypeResponses = new List<ProductTypeResponse>
            {
                _testProductTypeResponse,
                new ProductTypeResponse(2, "Chandelier")
            };

            _mockProductDAO
                .Setup(dao => dao.GetProducts(null, It.IsAny<ProductFilter>(), null))
                .ReturnsAsync(products);

            _mockProductTypeService
                .Setup(service => service.GetListProductTypeResponseByIds(It.IsAny<List<int>>()))
                .ReturnsAsync(productTypeResponses);

            var result = await _productService.GetAllProducts(null, new ProductFilter(), null);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));

            var resultList = result.ToList();
            Assert.That(resultList[0].Article, Is.EqualTo("TEST001"));
            Assert.That(resultList[1].Article, Is.EqualTo("TEST002"));

            _mockProductDAO.Verify(dao => dao.GetProducts(null, It.IsAny<ProductFilter>(), null), Times.Once);
            _mockProductTypeService.Verify(service => service.GetListProductTypeResponseByIds(It.IsAny<List<int>>()), Times.Once);
        }

        [Test]
        public async Task GetEmptyAllProducts()
        {
            var emptyProducts = new List<Product>();
            var emptyProductTypes = new List<ProductTypeResponse>();

            _mockProductDAO
                .Setup(dao => dao.GetProducts(null, It.IsAny<ProductFilter>(), null))
                .ReturnsAsync(emptyProducts);

            _mockProductTypeService
                .Setup(service => service.GetListProductTypeResponseByIds(It.IsAny<List<int>>()))
                .ReturnsAsync(emptyProductTypes);

            var result = await _productService.GetAllProducts(null, new ProductFilter(), null);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);

            _mockProductDAO.Verify(dao => dao.GetProducts(null, It.IsAny<ProductFilter>(), null), Times.Once);
        }

        [Test]
        public async Task GetAllProductsWithSearch()
        {
            var searchTerm = "lamp";
            var products = new List<Product> { _testProduct };

            var productTypeResponses = new List<ProductTypeResponse> { _testProductTypeResponse };

            _mockProductDAO
                .Setup(dao => dao.GetProducts(null, It.IsAny<ProductFilter>(), searchTerm))
                .ReturnsAsync(products);

            _mockProductTypeService
                .Setup(service => service.GetListProductTypeResponseByIds(It.IsAny<List<int>>()))
                .ReturnsAsync(productTypeResponses);

            var result = await _productService.GetAllProducts(null, new ProductFilter(), searchTerm);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));

            _mockProductDAO.Verify(dao => dao.GetProducts(null, It.IsAny<ProductFilter>(), searchTerm), Times.Once);
        }

        [Test]
        public async Task GetAllProductsWithThreeSortTypes()
        {
            var sortOptions = new[] { "price_asc", "price_desc", "title_asc" };

            foreach (var sort in sortOptions)
            {
                var products = new List<Product> { _testProduct };
                var productTypeResponses = new List<ProductTypeResponse> { _testProductTypeResponse };

                _mockProductDAO
                    .Setup(dao => dao.GetProducts(sort, It.IsAny<ProductFilter>(), null))
                    .ReturnsAsync(products);

                _mockProductTypeService
                    .Setup(service => service.GetListProductTypeResponseByIds(It.IsAny<List<int>>()))
                    .ReturnsAsync(productTypeResponses);

                var result = await _productService.GetAllProducts(sort, new ProductFilter(), null);

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(1));

                _mockProductDAO.Verify(dao => dao.GetProducts(sort, It.IsAny<ProductFilter>(), null), Times.Once);

                _mockProductDAO.Invocations.Clear();
                _mockProductTypeService.Invocations.Clear();
            }
        }

        [Test]
        public async Task GetAllProductsWithFilters()
        {
            var filters = new ProductFilter
            {
                price_min = 50,
                price_max = 150,
                product_type = "1"
            };

            var products = new List<Product> { _testProduct };
            var productTypeResponses = new List<ProductTypeResponse> { _testProductTypeResponse };

            _mockProductDAO
                .Setup(dao => dao.GetProducts(null, filters, null))
                .ReturnsAsync(products);

            _mockProductTypeService
                .Setup(service => service.GetListProductTypeResponseByIds(It.IsAny<List<int>>()))
                .ReturnsAsync(productTypeResponses);

            var result = await _productService.GetAllProducts(null, filters, null);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));

            _mockProductDAO.Verify(dao => dao.GetProducts(null, filters, null), Times.Once);
        }

        [Test]
        public async Task GetSingleProductResponseById()
        {
            Guid productId = _testProduct.GetId();

            _mockProductDAO
                .Setup(dao => dao.GetProductById(productId))
                .ReturnsAsync(_testProduct);

            _mockProductTypeService
                .Setup(service => service.GetSingleProductTypeResponseById(_testProduct.GetProductTypeId()))
                .ReturnsAsync(_testProductTypeResponse);

            var result = await _productService.GetSingleProductResponseById(productId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
            Assert.That(result.Article, Is.EqualTo("TEST001"));
            Assert.That(result.Title, Is.EqualTo("Test Product"));

            _mockProductDAO.Verify(dao => dao.GetProductById(productId), Times.Once);
            _mockProductTypeService.Verify(service => service.GetSingleProductTypeResponseById(_testProduct.GetProductTypeId()), Times.Once);
        }

        [Test]
        public void GetSingleProductResponseByInvalidId()
        {
            Guid invalidId = Guid.NewGuid();

            _mockProductDAO
                .Setup(dao => dao.GetProductById(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"Product with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _productService.GetSingleProductResponseById(invalidId));

            _mockProductDAO.Verify(dao => dao.GetProductById(invalidId), Times.Once);
        }

        [Test]
        public async Task CreateNewProduct()
        {
            Guid newProductId = Guid.NewGuid();

            _mockProductTypeService
                .Setup(service => service.GetSingleProductTypeById(_validProductRequest.ProductTypeId))
                .ReturnsAsync(_testProductType);

            _mockProductDAO
                .Setup(dao => dao.CreateProduct(It.IsAny<Product>()))
                .ReturnsAsync(newProductId);

            _mockWebHostEnvironment
                .Setup(env => env.WebRootPath)
                .Returns("/wwwroot");

            var result = await _productService.CreateNewProduct(_validProductRequest);

            Assert.That(result, Is.EqualTo(newProductId));

            _mockProductTypeService.Verify(service => service.GetSingleProductTypeById(_validProductRequest.ProductTypeId), Times.Once);
            _mockProductDAO.Verify(dao => dao.CreateProduct(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void CreateInvalidNewProduct()
        {
            _mockProductTypeService
                .Setup(service => service.GetSingleProductTypeById(_invalidProductRequest.ProductTypeId))
                .ThrowsAsync(new KeyNotFoundException("Product type not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _productService.CreateNewProduct(_invalidProductRequest));

            _mockProductTypeService.Verify(service => service.GetSingleProductTypeById(_invalidProductRequest.ProductTypeId), Times.Once);
        }

        [Test]
        public async Task DeleteSingleProductById()
        {
            Guid productId = _testProduct.GetId();

            _mockDeleteProductAttributeService
                .Setup(service => service.DeleteListProductAttributesByProductId(productId))
                .Returns(Task.CompletedTask);

            _mockProductDAO
                .Setup(dao => dao.DeleteProductById(productId))
                .Returns(Task.CompletedTask);

            await _productService.DeleteSingleProductById(productId);

            _mockDeleteProductAttributeService.Verify(service => service.DeleteListProductAttributesByProductId(productId), Times.Once);
            _mockProductDAO.Verify(dao => dao.DeleteProductById(productId), Times.Once);
        }

        [Test]
        public void DeleteSingleProductByInvalidId()
        {
            Guid invalidId = Guid.NewGuid();

            _mockDeleteProductAttributeService
                .Setup(service => service.DeleteListProductAttributesByProductId(invalidId))
                .Returns(Task.CompletedTask);

            _mockProductDAO
                .Setup(dao => dao.DeleteProductById(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"Product with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _productService.DeleteSingleProductById(invalidId));

            _mockProductDAO.Verify(dao => dao.DeleteProductById(invalidId), Times.Once);
        }

        [Test]
        public async Task UpdateSingleProductById()
        {
            Guid productId = _testProduct.GetId();

            _mockProductTypeService
                .Setup(service => service.GetSingleProductTypeById(_validProductRequest.ProductTypeId))
                .ReturnsAsync(_testProductType);

            _mockProductDAO
                .Setup(dao => dao.GetProductById(productId))
                .ReturnsAsync(_testProduct);

            _mockProductDAO
                .Setup(dao => dao.UpdateProduct(It.IsAny<Product>()))
                .ReturnsAsync(Guid.NewGuid());

            _mockWebHostEnvironment
                .Setup(env => env.WebRootPath)
                .Returns("/wwwroot");

            await _productService.UpdateSingleProductById(productId, _validProductRequest);

            _mockProductTypeService.Verify(service => service.GetSingleProductTypeById(_validProductRequest.ProductTypeId), Times.Once);
            _mockProductDAO.Verify(dao => dao.GetProductById(productId), Times.Once);
            _mockProductDAO.Verify(dao => dao.UpdateProduct(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void UpdateSingleProductByInvalidId()
        {
            Guid invalidId = Guid.NewGuid();

            _mockProductDAO
                .Setup(dao => dao.GetProductById(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"Product with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _productService.UpdateSingleProductById(invalidId, _validProductRequest));

            _mockProductDAO.Verify(dao => dao.GetProductById(invalidId), Times.Once);
        }
    }
}
