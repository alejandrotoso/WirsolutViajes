using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Client.Infrastructure.Managers
{
    public interface IValueManager : IManager
    {
        Task<IResult<List<VehicleTypeDTO>>> GetAllVehicleTypesAsync();
        Task<IResult<List<VehicleSubtypeDTO>>> GetSubtypeVehicleByVehicleTypeIdAsync(int vehicleTypeId);
        Task<IResult<List<BrandDTO>>> GetBrandsByVehicleTypeIdAsync(int vehicleTypeId);
        Task<IResult<BrandVehicleTypeDTO>> GetBrandVehicleTypeByIdAsync(int id);
        Task<IResult<List<ModelDTO>>> GetModelsByBrandVehicleTypeIdAsync(int brandId, int vehicleTypeId);

    }
}
