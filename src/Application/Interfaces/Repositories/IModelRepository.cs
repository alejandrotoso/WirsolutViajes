using WirsolutViajes.Domain.Entities;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Repositories
{
    public interface IModelRepository : IRepository<Model>
    {
        Task<Result<IEnumerable<ModelDTO>>> GetAllByBrandVehicleTypeIdAsync(int brandId, int vehicleTypeId);
    }
}
