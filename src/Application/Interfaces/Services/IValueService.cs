using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Application.Interfaces.Services
{
    public interface IValueService
    {
        Task<Result<IEnumerable<VehicleTypeDTO>>> GetAllVehicleTypesAsync();

        Task<Result<BrandVehicleTypeDTO>> GetBrandVehicleTypeByIdAsync(int id);

        Task<Result<IEnumerable<VehicleSubtypeDTO>>> GetVehicleSubtypesByVehicleTypeIdAsync(int vehicleTypeId);

        Task<Result<IEnumerable<BrandDTO>>> GetBrandsByVehicleTypeIdAsync(int vehicleTypeId);


        Task<Result<IEnumerable<ModelDTO>>> GetModelsByBrandVehicleTypeIdAsync(int brandId, int vehicleTypeId);
    }
}
