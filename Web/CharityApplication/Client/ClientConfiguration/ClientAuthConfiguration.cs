using Blazored.LocalStorage;
using Blazored.SessionStorage;
using CharityApplication.Client.Connection.Interfaces.Services;
using CharityApplication.Client.Connection.Services;
using CharityApplication.Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace CharityApplication.Client.ClientConfiguration
{
    public static class ClientAuthConfiguration
    {
        public static IServiceCollection AddClientAuthConfiguration(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
            services.AddAuthorizationCore();

            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<HttpInterceptorService>();
            return services;
        }
    }
}