using ServerAPI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Kantineapp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Dependency injection
builder.Services.AddSingleton<IEventService, EventServiceInMemory>();
builder.Services.AddSingleton<IOpgaveService, OpgaveServiceInMemory>();
builder.Services.AddSingleton<IUserService, UserServiceInMemory>();
await builder.Build().RunAsync();