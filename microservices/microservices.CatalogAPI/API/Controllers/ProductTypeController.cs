using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using microservices.CatalogAPI.Domain.Models;

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

            var response = productTypes.Select(productType => new ProductTypeResponse(productType.GetId(), productType.GetTitle()));

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductTypeResponse>> GetProductTypeById(int id)
        {
            var productType = await _productTypeService.GetSingleProductTypeById(id);

            var response = new ProductTypeResponse(productType.GetId(), productType.GetTitle());

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProductType([FromBody] ProductTypeRequest request)
        {
            ProductType newProductType = new ProductType(request.Title);

            int newProductTypId = await _productTypeService.CreateNewProductType(newProductType);

            return Ok(newProductTypId);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            await _productTypeService.DeleteSingleProductTypeById(id);

            return Ok();
        }
    }
}
