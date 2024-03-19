using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WirsolutViajes.Client.Extensions;
using WirsolutViajes.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);



builder.AddRootComponents();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.AddClientServices();
builder.Services.AddScoped<NotificationService>();

await builder.Build().RunAsync();
