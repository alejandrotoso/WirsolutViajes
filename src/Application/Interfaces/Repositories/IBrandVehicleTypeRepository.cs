using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Repositories
{
    public interface IBrandVehicleTypeRepository : IRepository<BrandVehicleType>
    {
        Task<Result<BrandVehicleTypeDTO>> GetByIdAsync(int id);
    }
}
