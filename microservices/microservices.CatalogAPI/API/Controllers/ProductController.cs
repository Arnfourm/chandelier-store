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
        private readonly IProductAttributeService _productAttributeService;
        private readonly IDeleteProductAttributeService _deleteProductAttributeService;

        public ProductController(
            IProductService productService, 
            IProductAttributeService productAttributeService,
            IDeleteProductAttributeService deleteProductAttributeService)
        {
            _productService = productService;
            _productAttributeService = productAttributeService;
            _deleteProductAttributeService = deleteProductAttributeService;
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

        [HttpGet("Attribute/{productId:Guid}")]
        public async Task<ActionResult<IEnumerable<ProductAttributeResponse>>> GetProductAttributes(Guid productId)
        {
            IEnumerable<ProductAttributeResponse> response = await _productAttributeService.GetProductAttributeByProductId(productId);

            return Ok(response);
        }

        [HttpPost("Attribute")]
        public async Task<ActionResult> CreateProductAttribute([FromBody] ProductAttributeRequest request)
        {
            await _productAttributeService.CreateNewSingleProductAttribute(request);

            return Ok();
        }

        [HttpPut("Attribute")]
        public async Task<ActionResult> UpdateProductAttribute([FromBody] ProductAttributeRequest request)
        {
            await _productAttributeService.UpdateSingleProductAttribute(request);

            return Ok();
        }

        [HttpDelete("{productId:Guid}/Attribute/{attributeId:Guid}")]
        public async Task<ActionResult> DeleteProductAttribute(Guid productId, Guid attributeId)
        {
            await _deleteProductAttributeService.DeleteSingleProductAttribute(productId, attributeId);

            return Ok();
        }
   }
}