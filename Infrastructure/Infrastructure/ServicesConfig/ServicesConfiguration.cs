using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.ServicesConfig
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IVerificationService, VerificationService>();
            services.AddScoped<IQrCodeService, QrCodeService>();

            services.AddScoped<IEmailService, EmailService>();
            services.Configure<EmailConfigurationModel>(configuration.GetSection("EmailConfiguration"))
                .AddSingleton(s => s.GetRequiredService<IOptions<EmailConfigurationModel>>().Value);

            return services;
        }
    }
}