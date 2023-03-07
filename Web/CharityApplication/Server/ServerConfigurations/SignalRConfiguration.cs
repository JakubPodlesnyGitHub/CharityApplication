using Microsoft.AspNetCore.ResponseCompression;

namespace CharityApplication.Server.ServerConfigurations
{
    public static class SignalRConfiguration
    {
        public static IServiceCollection AddSingalRConfiguration(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            return services;
        }
    }
}