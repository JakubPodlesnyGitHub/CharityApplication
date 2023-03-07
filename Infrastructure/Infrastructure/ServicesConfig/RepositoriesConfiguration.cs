using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServicesConfig
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection AddRepositoriesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPrivateAccountRepository, PrivateAccountRepository>();
            services.AddScoped<ICompanyAddressRepository, CompanyAddressRepository>();
            services.AddScoped<ICompanyRepresentativeRepository, CompanyRepresentativeRepository>();
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IEventRepository, EventRepostiory>();
            services.AddScoped<IBadgeAccountRepository, BadgeAccountRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupNameRepository, GroupNameRepository>();
            services.AddScoped<ICompanyAccountRepository, CompanyAccountRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IAssesmentFormRepository, AssesmentFormRepository>();
            services.AddScoped<IEventAccountRepository, EventAccountRepository>();
            services.AddScoped<IContactFormRepository, ContactFormRepository>();
            services.AddScoped<IGroupAccountRepository, GroupAccountRepository>();
            services.AddScoped<IBadgeGroupRepository, BadgeGroupRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IEventModuleRepository, EventModuleRepository>();
            services.AddScoped<IEventAnnouncementRepository, EventAnnouncementRepository>();
            services.AddScoped<IGroupAnnouncementRepository, GroupAnnouncementRepository>();
            services.AddScoped<IGroupEventRepository, GroupEventRepository>();

            return services;
        }
    }
}