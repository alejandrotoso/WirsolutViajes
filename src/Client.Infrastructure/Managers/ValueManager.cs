using WirsolutViajes.Client.Infrastructure.Extensions;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Client.Infrastructure.Managers
{
    public class ValueManager : IValueManager
    {
        private readonly HttpClient _httpClient;

        public ValueManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<List<VehicleTypeDTO>>> GetAllVehicleTypesAsync()
        {
            var response = await _httpClient.GetAsync(Routes.ValueEndpoints.GetAllVehicleTypes);
            return await response.ToResult<List<VehicleTypeDTO>>();
        }

        public async Task<IResult<List<VehicleSubtypeDTO>>> GetSubtypeVehicleByVehicleTypeIdAsync(int vehicleTypeId)
        {
            var response = await _httpClient.GetAsync(Routes.ValueEndpoints.GetVehicleSubtypesByVehicleTypeId(vehicleTypeId));
            return await response.ToResult<List<VehicleSubtypeDTO>>();
        }

        public async Task<IResult<List<BrandDTO>>> GetBrandsByVehicleTypeIdAsync(int vehicleTypeId)
        {
            var response = await _httpClient.GetAsync(Routes.ValueEndpoints.GetBrandsByVehicleTypeId(vehicleTypeId));
            return await response.ToResult<List<BrandDTO>>();
        }

        public async Task<IResult<List<ModelDTO>>> GetModelsByBrandVehicleTypeIdAsync(int brandId, int vehicleTypeId)
        {
            var response = await _httpClient.GetAsync(Routes.ValueEndpoints.GetModelsByBrandVehicleTypeId(brandId, vehicleTypeId));
            return await response.ToResult<List<ModelDTO>>();
        }

        public async Task<IResult<BrandVehicleTypeDTO>> GetBrandVehicleTypeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(Routes.ValueEndpoints.GetBrandVehicleTypeById(id));
            return await response.ToResult<BrandVehicleTypeDTO>();
        }
    }
}
