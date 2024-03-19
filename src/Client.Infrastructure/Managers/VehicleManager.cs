using System.Net.Http.Json;
using WirsolutViajes.Client.Infrastructure.Extensions;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Client.Infrastructure.Managers
{
    public class VehicleManager : IVehicleManager
    {
        private readonly HttpClient _httpClient;

        public VehicleManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<VehicleDTO>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.VehicleEndpoints.GetAll);
            return await response.ToResult<List<VehicleDTO>>();
        }

        public async Task<IResult<VehicleDTO>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(Routes.VehicleEndpoints.GetById(id));
            return await response.ToResult<VehicleDTO>();
        }

        public async Task<IResult<List<VehicleDTO>>> GetByTypeAndVehicleSubtypeIdAsync(int vehicleTypeId, int vehicleSubtypeId)
        {
            var response = await _httpClient.GetAsync(Routes.VehicleEndpoints.GetByTypeAndVehicleSubtypeId(vehicleTypeId, vehicleSubtypeId));
            return await response.ToResult<List<VehicleDTO>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditVehicleDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.VehicleEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}