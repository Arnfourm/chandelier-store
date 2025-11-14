using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservice.SupplyAPI.API.Controllers
{
    [ApiController]
    [Route("api/Supply/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierResponse>>> GetSuppliers()
        {
            IEnumerable<SupplierResponse> response = await _supplierService.GetAllSuppliers();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSupplier([FromBody] SupplierRequest request)
        {
            await _supplierService.CreateNewSupplier(request);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateSupplier(Guid id, [FromBody] SupplierRequest request)
        {
            await _supplierService.UpdateSingleSupplier(id, request);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteSupplier(Guid id)
        {
            await _supplierService.DeleteSingleSupplierById(id);

            return Ok();
        }
    }
}
