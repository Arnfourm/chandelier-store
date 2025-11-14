using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.CatalogAPI.API.Controllers
{
    [ApiController]
    [Route("api/Catalog/[controller]")]
    public class MeasurementUnitController : ControllerBase
    {
        private readonly IMeasurementUnitService _measurementUnitService;

        public MeasurementUnitController(IMeasurementUnitService measurementUnitService)
        {
            _measurementUnitService = measurementUnitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementUnitResponse>>> GetMeasurementUnits()
        {
            IEnumerable<MeasurementUnitResponse> response = await _measurementUnitService.GetAllMeasurementUnits();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMeasurementUnit([FromBody] MeasurementUnitRequest request)
        {
            await _measurementUnitService.CreateNewMeasurementUnit(request);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateMeasurementUnit(int id, [FromBody] MeasurementUnitRequest request)
        {
            await _measurementUnitService.UpdateSingleMeasurementUnit(id, request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMeasurementUnit(int id)
        {
            await _measurementUnitService.DeleteSingleMeasurementUnitById(id);

            return Ok();
        }
    }
}
