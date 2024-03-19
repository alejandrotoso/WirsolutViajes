
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using WirsolutViajes.Client.Infrastructure.Managers;
using WirsolutViajes.Client.Infrastructure.Services;

namespace WirsolutViajes.Client.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        private const string ClientName = "WirsolutViajes.API";

        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            builder
                .Services
                .AddMudServices(configuration =>
                {
                    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                    configuration.SnackbarConfiguration.PreventDuplicates = false;
                    configuration.SnackbarConfiguration.NewestOnTop = false;
                    configuration.SnackbarConfiguration.ShowCloseIcon = true;
                    configuration.SnackbarConfiguration.VisibleStateDuration = 20000;
                    configuration.SnackbarConfiguration.HideTransitionDuration = 500;
                    configuration.SnackbarConfiguration.ShowTransitionDuration = 500;
                    configuration.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
                })

                .AddManagers()

                .AddTransient<IWeatherAPIService, WeatherAPIService>()
                .AddTransient<IContryService, ContryService>();

            return builder;
        }

        private static IServiceCollection AddManagers(this IServiceCollection services)
        {

            services.AddTransient<IValueManager, ValueManager>();
            services.AddTransient<IVehicleManager, VehicleManager>();
            services.AddTransient<ICityManager, CityManager>();
            services.AddTransient<ITripManager, TripManager>();

            return services;
        }

    }
}
