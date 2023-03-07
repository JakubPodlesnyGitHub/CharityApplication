namespace CharityApplication.Client.ClientConfiguration
{
    public static class ClientSetup
    {
        public static IServiceCollection AddClientSetup(this IServiceCollection services)
        {
            services.AddClientServicesRepositoryConfiguration();
            services.AddClientAuthConfiguration();
            return services;
        }
    }
}