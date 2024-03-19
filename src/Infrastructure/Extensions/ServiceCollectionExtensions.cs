using Microsoft.Extensions.DependencyInjection;
using WirsolutViajes.Application.Interfaces.Repositories;
using WirsolutViajes.Infrastructure.Repositories;
using WirsolutViajes.Server.Infrastructure.Persistence.Repositories;

namespace WirsolutViajes.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepository<>), typeof(Repository<>))


                .AddTransient<ICityRepository, CityRepository>()

                .AddTransient<IVehicleTypeRepository, VehicleTypeRepository>()
                .AddTransient<IVehicleSubtypeRepository, VehicleSubtypeRepository>()
                
                .AddTransient<IBrandVehicleTypeRepository, BrandVehicleTypeRepository>()
                .AddTransient<IBrandRepository, BrandRepository>()
                .AddTransient<IModelRepository, ModelRepository>()

                .AddTransient<IVehicleRepository, VehicleRepository>()
                .AddTransient<ITripRepository, TripRepository>();

        }
    }
}