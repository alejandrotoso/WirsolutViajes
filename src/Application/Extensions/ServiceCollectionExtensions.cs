using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Application.Services;

namespace WirsolutViajes.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IValueService, ValueService>();
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<ITripService, TripService>();

            return services;
        }
    }
}