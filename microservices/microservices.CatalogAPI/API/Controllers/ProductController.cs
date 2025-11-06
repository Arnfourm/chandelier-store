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

       public ProductController(IProductService productService)
       {
           _productService = productService;
       }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();

            var response = products
                .Select(product => new Product(
                    product.GetId(),
                    product.GetArticle(),
                    product.GetTitle(),
                    product.GetPrice(),
                    product.GetQuantity(),
                    product.GetProductTypeId(),
                    product.GetAddedDate()
                ));

            return Ok(response);
        }
   }
}