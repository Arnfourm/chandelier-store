using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.Services;
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
        public async Task<ActionResult<IEnumerable<SupplyResponse>>> GetSupplies()
        {
            IEnumerable<SupplyResponse> response = await _supplyService.GetAllSupplies();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSupply([FromBody] SupplyRequest request)
        {
            await _supplyService.CreateNewSupply(request);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteSupply(Guid id)
        {
            await _supplyDeleteService.DeleteSingleSupplyById(id);

            return Ok();
        }

        [HttpGet("{supplyId:Guid}/Product/")]
        public async Task<ActionResult<IEnumerable<SupplyProductResponse>>> GetSupplyProductsById(Guid supplyId)
        {
            IEnumerable<SupplyProductResponse> response = await _supplyProductService.GetListSupplyProductBySupplyId(supplyId);

            return Ok(response);
        }

        [HttpPost("Product")]
        public async Task<ActionResult> CreateSupplyProduct([FromBody] SupplyProductRequest request)
        {
            await _supplyProductService.CreateNewSupplyProduct(request);

            return Ok();
        }

        [HttpDelete("{supplyId:Guid}/Product/{productId:Guid}")]
        public async Task<ActionResult> DeleteSupplyProduct(Guid supplyId, Guid productId)
        {
            await _supplyProductService.DeleteSupplyProductByBothIds(supplyId, productId);

            return Ok();
        }
    }
}