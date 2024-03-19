using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<Result<IEnumerable<VehicleDTO>>> GetAllVehiclesAsync();

        Task<Result<VehicleDTO>> GetByIdAsync(int id);

        Task<Result<IEnumerable<VehicleDTO>>> GetByTypeAndVehicleSubtypeIdAsync(int vehicleTypeId, int vehicleSubtypeId);
        
        Task<Result<int>> SaveAsync(AddEditVehicleDTO addEditVehicleDTO);
    }
}
