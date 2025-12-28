using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace microservices.CatalogAPI.API.Controllers
{
    [ApiController]
    [Route("api/Catalog/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService) {
            _productTypeService = productTypeService; 
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductTypeResponse>>> GetProductTypes()
        {
            IEnumerable<ProductTypeResponse> response = await _productTypeService.GetAllProductTypes();

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<int>> CreateProductType([FromBody] ProductTypeRequest request)
        {
            int newProductTypId = await _productTypeService.CreateNewProductType(request);

            return Ok(newProductTypId);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<int>> UpdateProductType([FromBody] ProductTypeRequest request, int id)
        {
            int updateProductTypeId = await _productTypeService.UpdateSingleProductType(id, request);

            return Ok(updateProductTypeId);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            await _productTypeService.DeleteSingleProductTypeById(id);

            return Ok();
        }
    }
}
