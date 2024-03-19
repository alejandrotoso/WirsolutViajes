using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Repositories
{
    public interface IVehicleTypeRepository : IRepository<VehicleType>
    {
        Task<Result<IEnumerable<VehicleTypeDTO>>> GetAllAsync();
    }
}
