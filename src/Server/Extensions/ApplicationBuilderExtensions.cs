using WirsolutViajes.Server.Middlewares;

namespace WirsolutViajes.Server.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            return app;
        }


        internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }


        //internal static IApplicationBuilder UseForwarding(this IApplicationBuilder app, IConfiguration configuration)
        //{
        //    AppConfiguration config = GetApplicationSettings(configuration);
        //    if (config.BehindSSLProxy)
        //    {
        //        app.UseCors();
        //        app.UseForwardedHeaders();
        //    }

        //    return app;
        //}


        //internal static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
        //    => app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapRazorPages();
        //        endpoints.MapControllers();
        //        endpoints.MapFallbackToFile("index.html");
        //        //endpoints.MapHub<SignalRHub>(ApplicationConstants.SignalR.HubUrl);
        //    });


    }
}