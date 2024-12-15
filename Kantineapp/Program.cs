using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kantineapp.Services;
using Kantineapp;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Konfigurer HttpClient med global JsonSerializerOptions
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5079/")
    };

    // Global JSON-indstillinger til HttpClient
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    return httpClient;
});

// Dependency injection
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IOpgaveService, OpgaveService>();
builder.Services.AddScoped<IBrugerService, BrugerService>();

await builder.Build().RunAsync();