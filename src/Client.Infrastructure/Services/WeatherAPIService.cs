using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using WirsolutViajes.Client.Infrastructure.DTOs;


namespace WirsolutViajes.Client.Infrastructure.Services
{
    public class WeatherAPIService : IWeatherAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly IContryService _paisService;
        
        public WeatherAPIService(HttpClient httpClient, IContryService paisService)
        {
            _httpClient = httpClient;
            _paisService = paisService;
        }

        public async Task<List<LocationResponse>> GetLocations(string location)
        {
            //api2019api
            var apiKey = "acf5937d278e2e42ab5d63d772e60ef6";
            var url = $"https://api.openweathermap.org/geo/1.0/direct?q={location}&limit=10&appid={apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var locationResponseArray = await response.Content.ReadFromJsonAsync<LocationResponse[]>();

                // Convierte el array en una lista
                var locationResponseList = new List<LocationResponse>(locationResponseArray);

                foreach (var item in locationResponseList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.CountryName = _paisService.ObtenerNombrePais(item.Country);
                }

                return locationResponseList;
            }
            else
            {
                throw new Exception($"Failed to fetch weather locations. Status code: {response.StatusCode}");
            }
        }

        public async Task<List<DailyForecast>> GetFiveDayForecast(double latitude, double longitude)
        {
            var apiKey = "acf5937d278e2e42ab5d63d772e60ef6";

            var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&lang=es&units=metric&appid={apiKey}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var forecastResponse = JsonSerializer.Deserialize<ForecastResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });



            //foreach (var forecastData in forecastResponse.List)
            //{
            //    Console.WriteLine($"Fecha y hora: {forecastData.dt_txt}");
            //    Console.WriteLine($"Temperatura: {forecastData.Main.Temp}");
            //    Console.WriteLine($"Descripción del clima: {forecastData.Weather[0].Description}");
            //    Console.WriteLine($"Velocidad del viento: {forecastData.Wind.Speed}");
            //    Console.WriteLine($"Visibilidad: {forecastData.Visibility}");
            //    Console.WriteLine($"Probabilidad de precipitación: {forecastData.Pop}");
            //    Console.WriteLine("---------------------------------------------");
            //}



            var dailyForecasts = forecastResponse.List
                .GroupBy(x => DateTime.ParseExact(x.dt_txt, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).Date)
                .Select(group =>
                {
                    var maxTemp = group.Max(x => x.Main.Temp);
                    var minTemp = group.Min(x => x.Main.Temp);

                    var maxPopItem = group.OrderByDescending(x => x.Pop).FirstOrDefault();
                    var weatherDescription = maxPopItem?.Weather.FirstOrDefault()?.Description;
                    var icon = maxPopItem?.Weather.FirstOrDefault()?.Icon;

                    return new DailyForecast
                    {
                        Date = group.Key,
                        MaxTemp = maxTemp,
                        MinTemp = minTemp,
                        MaxRainProbability = group.Max(x => x.Pop),
                        Description = weatherDescription,
                        Icon = icon
                    };
                })
                .ToList();


            //foreach (var forecast in dailyForecasts)
            //{
            //    Console.WriteLine($"Date: {forecast.Date:yyyy-MM-dd}");
            //    Console.WriteLine($"Max Temp: {forecast.MaxTemp}°C");
            //    Console.WriteLine($"Min Temp: {forecast.MinTemp}°C");
            //    Console.WriteLine($"Max Rain Probability: {forecast.MaxRainProbability}");
            //    Console.WriteLine($"Description: {forecast.Description}");
            //    Console.WriteLine();
            //}


            return dailyForecasts;

        }
    }
}
