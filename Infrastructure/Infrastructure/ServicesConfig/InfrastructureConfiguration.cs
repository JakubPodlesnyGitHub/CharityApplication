using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.ServicesConfig
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddRepositoriesConfiguration();
            services.AddServicesConfiguration(configuration);

            services.AddAuthConfiguration(configuration);

            return services;
        }
    }
}