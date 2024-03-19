using System.Net.Http.Json;
using WirsolutViajes.Client.Infrastructure.Extensions;
using WirsolutViajes.Shared.DTOs;
using WirsolutViajes.Shared.Wrapper;

namespace WirsolutViajes.Client.Infrastructure.Managers
{
    public class CityManager : ICityManager
    {
        private readonly HttpClient _httpClient;

        public CityManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IResult<List<CityDTO>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.CityEndpoints.GetAll);
            return await response.ToResult<List<CityDTO>>();
        }

        public async Task<IResult<List<CityDTO>>> GetAllActiveAsync()
        {
            var response = await _httpClient.GetAsync(Routes.CityEndpoints.GetAllActive);
            return await response.ToResult<List<CityDTO>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditCityDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.CityEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}