using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<Result<IEnumerable<VehicleDTO>>> GetAllAsync();

        Task<Result<VehicleDTO>> GetByIdAsync(int id);
        Task<Result<IEnumerable<VehicleDTO>>> GetByTypeAndVehicleSubtypeIdAsync(int vehicleTypeId, int vehicleSubtypeId);

        Task<Result<int>> SaveAsync(AddEditVehicleDTO addEditVehicleDTO);
    }
}
