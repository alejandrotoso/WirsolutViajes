using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Repositories
{
    public interface IVehicleSubtypeRepository : IRepository<VehicleSubtype>
    {
        Task<Result<IEnumerable<VehicleSubtypeDTO>>> GetAllByVehicleTypeIdAsync(int vehicleTypeId);
    }
}
