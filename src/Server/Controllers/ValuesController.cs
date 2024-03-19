using Microsoft.AspNetCore.Mvc;
using WirsolutViajes.Application.Interfaces.Services;

namespace WirsolutViajes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValueService _valueService;

        public ValuesController(IValueService valueService)
        {
            _valueService = valueService;
        }

        [HttpGet("all-vehicle-types")]
        public async Task<IActionResult> GetAllVehicleTypes()
        {
            var result = await _valueService.GetAllVehicleTypesAsync();
            return Ok(result);
        }

        [HttpGet("vehicle-subtypes-by-vehicle-type-id/{vehicleTypeId}")]
        public async Task<IActionResult> GetVehicleSubtypesByVehicleTypeId(int vehicleTypeId)
        {
            var result = await _valueService.GetVehicleSubtypesByVehicleTypeIdAsync(vehicleTypeId);
            return Ok(result);
        }

        [HttpGet("brands-by-vehicle-type-id/{vehicleTypeId}")]
        public async Task<IActionResult> GetBrandsByVehicleTypeId(int vehicleTypeId)
        {
            var result = await _valueService.GetBrandsByVehicleTypeIdAsync(vehicleTypeId);
            return Ok(result);
        }

        [HttpGet("models-by-brand-id-and-vehicle-type-id/{brandId}/{vehicleTypeId}")]
        public async Task<IActionResult> GetModelsByBrandVehicleTypeId(int brandId, int vehicleTypeId)
        {
            var result = await _valueService.GetModelsByBrandVehicleTypeIdAsync(brandId, vehicleTypeId);
            return Ok(result);
        }

        [HttpGet("brand-vehicle-type-by-id/{id}")]
        public async Task<IActionResult> GetBrandVehicleTypeById(int id)
        {
            var result = await _valueService.GetBrandVehicleTypeByIdAsync(id);
            return Ok(result);
        }
    }
}
