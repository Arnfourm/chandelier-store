using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

namespace microservices.CatalogAPI.API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductController : ControllerBase
   {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IProductTypeService productTypeService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            IEnumerable<ProductResponse> response = await _productService.GetAllProducts();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] ProductRequest request)
        {
            Guid newProductId = await _productService.CreateNewProduct(request);

            return Ok(newProductId);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest request)
        {
            await _productService.UpdateSingleProductById(id, request);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteSingleProductById(id);

            return Ok();
        }
   }
}