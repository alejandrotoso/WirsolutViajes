using WirsolutViajes.Client.Infrastructure.DTOs;

namespace WirsolutViajes.Client.Infrastructure.Services
{
    public interface IWeatherAPIService
    {
        Task<List<DailyForecast>> GetFiveDayForecast(double latitude, double longitude);
        Task<List<LocationResponse>> GetLocations(string location);
    }
}
