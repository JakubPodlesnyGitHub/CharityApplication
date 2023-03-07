using CharityApplication.Client;
using CharityApplication.Client.ClientConfiguration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
}.EnableIntercept(sp));

builder.Services.AddHttpClientInterceptor();

builder.Services.AddClientSetup();

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();