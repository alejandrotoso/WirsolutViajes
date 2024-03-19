using Microsoft.AspNetCore.Mvc;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Shared.DTOs;

namespace WirsolutViajes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _vehicleService.GetAllVehiclesAsync();
            return Ok(result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _vehicleService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("by-type-and-subtype-vehicle/{vehicleTypeId}/{vehicleSubtypeId}")]
        public async Task<IActionResult> GetByMarcaVehicleTypeId(int vehicleTypeId, int vehicleSubtypeId)
        {
            var result = await _vehicleService.GetByTypeAndVehicleSubtypeIdAsync(vehicleTypeId, vehicleSubtypeId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddEditVehicleDTO addEditVehicleDTO)
        {
            return Ok(await _vehicleService.SaveAsync(addEditVehicleDTO));
        }
    }
}
