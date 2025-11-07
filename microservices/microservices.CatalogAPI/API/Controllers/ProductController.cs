using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace microservices.CatalogAPI.API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductController : ControllerBase
   {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;

        public ProductController(IProductService productService, IProductTypeService productTypeService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();

            var response = products
                .Select(product => 
                    ProductType productType = _productTypeService.GetSingleProductTypeByTitle(product.GetProductTypeId());
                    new ProductResponse(
                        product.GetId(),
                        product.GetArticle(),
                        product.GetTitle(),
                        product.GetPrice(),
                        product.GetQuantity(),
                        productType.GetId(),
                        product.GetAddedDate()
                    )
                );

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] ProductRequest request)
        {
            Product newProduct = new Product(
                request.Article,
                request.Title,
                request.Price,
                request.Quantity,
                request.ProductType,
                request.AddedDate
            );

            int newProductId = await _productDAO.CreateNewProduct(newProduct);

            return Ok(newProductId);
        }

        [HttpDelete("id:Guid")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteSingleProductById(id);

            return Ok();
        }
   }
}