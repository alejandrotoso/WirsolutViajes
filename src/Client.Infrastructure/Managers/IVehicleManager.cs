using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Client.Infrastructure.Managers
{
    public interface IVehicleManager : IManager
    {
        Task<IResult<List<VehicleDTO>>> GetAllAsync();
        Task<IResult<VehicleDTO>> GetByIdAsync(int id);
        Task<IResult<List<VehicleDTO>>> GetByTypeAndVehicleSubtypeIdAsync(int vehicleTypeId, int vehicleSubtypeId);
        Task<IResult<int>> SaveAsync(AddEditVehicleDTO request);
    }
}