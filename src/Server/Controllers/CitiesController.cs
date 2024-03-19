using Microsoft.AspNetCore.Mvc;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Shared.DTOs;

namespace WirsolutViajes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cityService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var result = await _cityService.GetAllActiveAsync();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post(AddEditCityDTO addEditCityDTO)
        {
            return Ok(await _cityService.SaveAsync(addEditCityDTO));
        }
    }
}
