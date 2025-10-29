using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.CatalogAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly ProductTypeService _productTypeService;

        public ProductTypeController(ProductTypeService productTypeService) {
            _productTypeService = productTypeService; 
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductTypeResponse>>> GetProductTypes()
        {
            var productTypes = await _productTypeService.GetAllProductTypes();

            var response = productTypes.Select(productType => new ProductTypeResponse(productType.GetId(), productType.GetTitle()));

            return Ok(response);
        }
    }
}
