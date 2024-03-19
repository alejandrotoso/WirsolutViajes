using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IResult<IEnumerable<CityDTO>>> GetAllActiveAsync()
        {
            return await _cityRepository.GetAllActiveAsync();
        }

        public async Task<Result<IEnumerable<CityDTO>>> GetAllAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<Result<int>> SaveAsync(AddEditCityDTO addEditCityDTO)
        {
            return await _cityRepository.SaveAsync(addEditCityDTO);

        }
    }
}

