using Application.Interfaces.Services;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static IdentityModel.ClaimComparer;

namespace Infrastructure.ServicesConfig
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtTokenConfigurationModel>(configuration.GetSection("JwtSettings"))
               .AddSingleton(s => s.GetRequiredService<IOptions<JwtTokenConfigurationModel>>().Value);

            services.Configure<GoogleAuthConfigurationModel>(configuration.GetSection("GoogleAuth"))
               .AddSingleton(s => s.GetRequiredService<IOptions<GoogleAuthConfigurationModel>>().Value);

            services.AddScoped<ITokenService, TokenService>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //opt.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                //opt.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddIdentityServerJwt()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromSeconds(20),

                    ValidIssuer = configuration.GetSection("JwtSettings")["ValidIssuer"],
                    ValidAudience = configuration.GetSection("JwtSettings")["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings")["SecurityKey"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            //.AddGoogle(googleOpt =>
            //{
            //    googleOpt.ClientId = configuration.GetSection("GoogleAuth")["ClientId"];
            //    googleOpt.ClientSecret = configuration.GetSection("GoogleAuth")["ClientSecret"];
            //    googleOpt.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
            //    googleOpt.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
            //    googleOpt.Scope.Add("https://www.googleapis.com/auth/user.phonenumbers.read");
            //    googleOpt.Scope.Add("https://www.googleapis.com/auth/user.birthday.read");
            //});
            return services;
        }
    }
}