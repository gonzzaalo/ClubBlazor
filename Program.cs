using ClubBlazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CurrieTechnologies.Razor.SweetAlert2;
using ClubBlazor.Interfaces;
using ClubBlazor.Models;



internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
        var urlApi = builder.Configuration.GetValue<string>("urlApi");

        // Registra HttpClient con la URL de la API
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(urlApi) });

        // Registra GenericService e IGenericService
        builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        builder.Services.AddScoped<GenericService<Deporte>>();
        builder.Services.AddScoped<GenericService<Profesor>>();
        builder.Services.AddScoped<GenericService<Deportista>>();
        builder.Services.AddScoped<GenericService<Socio>>();
        builder.Services.AddScoped<GenericService<Cuota>>();
        builder.Services.AddScoped<FirebaseAuthService>();
        







        // Registra HttpClient con la URL de la API
        builder.Services.AddSweetAlert2();
        await builder.Build().RunAsync();
    }
}