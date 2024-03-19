using System.Text.Json.Serialization;
using WirsolutViajes.Application.Extensions;
using WirsolutViajes.Infrastructure.Extensions;
using WirsolutViajes.Server.Extensions;
using WirsolutViajes.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddRazorPages();

builder.Services.AddApplicationLayer();
builder.Services.AddApplicationServices();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddRepositories();

//builder.Services.AddInfrastructureMappings();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//***
app.UseExceptionHandling(app.Environment);

app.UseHttpsRedirection();

//***
app.UseExceptionMiddleware();
//app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// Aquí se agrega el middleware
app.UseMiddleware<DataTypeValidationMiddleware>();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
