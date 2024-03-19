using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Result<IEnumerable<BrandDTO>>> GetAllByVehicleTypeIdAsync(int vehicleTypeId);
    }
}
