using Core.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kantineapp;
using Core.Services.Login;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Dependency injection
builder.Services.AddSingleton<IEventService, EventServiceInMemory>();
builder.Services.AddScoped<ILoginService, LoginService>();


await builder.Build().RunAsync();
