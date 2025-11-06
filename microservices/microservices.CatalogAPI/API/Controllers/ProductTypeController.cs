using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace microservices.CatalogAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService) {
            _productTypeService = productTypeService; 
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductTypeResponse>>> GetProductTypes()
        {
            var productTypes = await _productTypeService.GetAllProductTypes();

            var response = productTypes
                .Select(productType => new ProductTypeResponse(productType.GetId(), productType.GetTitle()));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProductType([FromBody] ProductTypeRequest request)
        {
            ProductType newProductType = new ProductType(request.Title);

            int newProductTypId = await _productTypeService.CreateNewProductType(newProductType);

            return Ok(newProductTypId);
        }

        [HttpPut("id:int")]
        public async Task<ActionResult<int>> UpdateProductType([FromBody] ProductTypeRequest request, int id)
        {
            ProductType updatedProductType = new ProductType(id, request.Title);

            int updatedProductTypeId = await _productTypeService.UpdateSingleProductType(updatedProductType);

            return Ok(updatedProductTypeId);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            await _productTypeService.DeleteSingleProductTypeById(id);

            return Ok();
        }
    }
}
