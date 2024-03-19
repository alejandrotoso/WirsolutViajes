using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<Result<IEnumerable<CityDTO>>> GetAllAsync();
        Task<IResult<IEnumerable<CityDTO>>> GetAllActiveAsync();
        Task<Result<int>> SaveAsync(AddEditCityDTO addEditCityDTO);
    }
}
