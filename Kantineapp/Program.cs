using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kantineapp.Services;
using Kantineapp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Dependency injection
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IOpgaveService, OpgaveService>();
builder.Services.AddScoped<IBrugerService, BrugerService>();


await builder.Build().RunAsync();