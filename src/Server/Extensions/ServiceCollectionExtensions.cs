using Microsoft.EntityFrameworkCore;
using WirsolutViajes.Application.Interfaces.Services;
using WirsolutViajes.Application.Services;
using WirsolutViajes.Server.Infrastructure.Persistence.Contexts;

namespace WirsolutViajes.Server.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));


            return services;
        }

        internal static IServiceCollection AddCurrentUserService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            //services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }

     
    }
}