using System.Net.Http.Json;
using WirsolutViajes.Client.Infrastructure.Extensions;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Client.Infrastructure.Managers
{
    public class TripManager : ITripManager
    {
        private readonly HttpClient _httpClient;

        public TripManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IResult<List<TripDTO>>> GetAllAsync(DateTime date, int destinationId, int vehicleTypeId)
        {
            var response = await _httpClient.GetAsync(Routes.TripEndpoints.GetAll(date, destinationId, vehicleTypeId));
            return await response.ToResult<List<TripDTO>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditTripDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.TripEndpoints.Save, request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.TripEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }
    }
}