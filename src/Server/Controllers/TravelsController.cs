using Microsoft.AspNetCore.Mvc;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TravelsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int destinationId,
            [FromQuery] int vehicleTypeId, [FromQuery] DateTime tripDate)
        {
            var result = await _tripService.GetFilteredTripsAsync(tripDate, destinationId, vehicleTypeId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddEditTripDTO addEditTripDTO)
        {
            return Ok(await _tripService.SaveAsync(addEditTripDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _tripService.DeleteAsync(id));
        }
    }
}
