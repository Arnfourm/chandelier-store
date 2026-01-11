using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservice.SupplyAPI.API.Controllers
{
    [ApiController]
    [Route("api/Supply/[controller]")]
    public class SupplyController : ControllerBase
    {
        private readonly ISupplyService _supplyService;
        private readonly ISupplyProductService _supplyProductService;
        private readonly ISupplyDeleteService _supplyDeleteService;

        public SupplyController(
            ISupplyService supplyService, 
            ISupplyProductService supplyProductService,
            ISupplyDeleteService supplyDeleteService
        )
        {
            _supplyService = supplyService;
            _supplyProductService = supplyProductService;
            _supplyDeleteService = supplyDeleteService;
        }

        [HttpGet]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<IEnumerable<SupplyResponse>>> GetSupplies()
        {
            IEnumerable<SupplyResponse> response = await _supplyService.GetAllSupplies();

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<SupplyResponse>> CreateSupply([FromBody] SupplyRequest request)
        {
            SupplyResponse response = await _supplyService.CreateNewSupply(request);

            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> DeleteSupply(Guid id)
        {
            await _supplyDeleteService.DeleteSingleSupplyById(id);

            return Ok();
        }

        [HttpGet("{supplyId:Guid}/Product/")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<IEnumerable<SupplyProductResponse>>> GetSupplyProductsById(Guid supplyId)
        {
            IEnumerable<SupplyProductResponse> response = await _supplyProductService.GetListSupplyProductBySupplyId(supplyId);

            return Ok(response);
        }

        [HttpPost("Product")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> CreateSupplyProduct([FromBody] SupplyProductRequest request)
        {
            await _supplyProductService.CreateNewSupplyProduct(request);

            return Ok();
        }

        [HttpDelete("{supplyId:Guid}/Product/{productId:Guid}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> DeleteSupplyProduct(Guid supplyId, Guid productId)
        {
            await _supplyProductService.DeleteSupplyProductByBothIds(supplyId, productId);

            return Ok();
        }
    }
}