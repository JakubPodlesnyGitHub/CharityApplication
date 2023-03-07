using CharityApplication.Client.Connection.Interfaces.Repositories;
using CharityApplication.Client.Connection.Interfaces.Services;
using CharityApplication.Client.Connection.Repositories;
using CharityApplication.Client.Connection.Services;
using CharityApplication.Client.Helpers.Http;
using MudBlazor;
using MudBlazor.Services;

namespace CharityApplication.Client.ClientConfiguration
{
    public static class ClientServiceConfiguration
    {
        public static IServiceCollection AddClientServicesRepositoryConfiguration(this IServiceCollection services)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.ShowCloseIcon = false;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.VisibleStateDuration = 2500;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
            });

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IContactFormRepository, ContactFromRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupNameRepository, GroupNameRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IEventModuleRepository, EventModuleRepository>();
            services.AddScoped<IEventAccountRepository, EventAccountRepository>();
            services.AddScoped<IGroupAccountRepository, GroupAccoutRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEventAnnouncementRepository, EventAnnouncementRepository>();
            services.AddScoped<IGroupAnnouncementRepository, GroupAnnouncementReposiotry>();
            services.AddScoped<IVerificationService, VerificationService>();
            services.AddScoped<IQrCodeService, QrCodeService>();
            services.AddScoped<IBadgeAccountRepository, BadgeAccountRepository>();
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IAssessmentFormRepository, AssessmentFormRepository>();
            services.AddScoped<IBadgeGroupRepository, BadgeGroupRepository>();
            return services;
        }
    }
}