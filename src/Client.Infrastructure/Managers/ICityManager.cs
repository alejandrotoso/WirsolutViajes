using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Client.Infrastructure.Managers
{
    public interface ICityManager : IManager
    {
        Task<IResult<List<CityDTO>>> GetAllAsync();
        Task<IResult<List<CityDTO>>> GetAllActiveAsync();
        Task<IResult<int>> SaveAsync(AddEditCityDTO request);
    }
}