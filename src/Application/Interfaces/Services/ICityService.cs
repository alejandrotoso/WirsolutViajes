using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Services
{
    public interface ICityService
    {
        Task<Result<IEnumerable<CityDTO>>> GetAllAsync();

        Task<IResult<IEnumerable<CityDTO>>> GetAllActiveAsync();

        Task<Result<int>> SaveAsync(AddEditCityDTO addEditCityDTO);
    }
}
