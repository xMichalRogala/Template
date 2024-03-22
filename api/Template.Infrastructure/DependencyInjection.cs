using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Template.Application.Core.Abstractions.Auth;
using Template.Application.Core.Abstractions.Commons;
using Template.Infrastructure.Core.Auth;
using Template.Infrastructure.Core.Auth.Settings;
using Template.Infrastructure.Core.Commons;

namespace Template.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

            services.AddAuth(configuration);

            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSettings jwtSettings = new();
            configuration.GetSection(JwtSettings.SettingsKey).Bind(jwtSettings);

            services.AddAuthentication()
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityAccessTokenKey))
                    };
                })
                .AddJwtBearer(JwtSettings.RefreshJwtTokenSchema, opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityRefreshTokenKey))
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();

                var onlySecondJwtSchemePolicyBuilder = new AuthorizationPolicyBuilder(JwtSettings.RefreshJwtTokenSchema);
                opt.AddPolicy(JwtSettings.RefreshJwtTokenSchema, onlySecondJwtSchemePolicyBuilder
                    .RequireAuthenticatedUser()
                    .Build());
            });

            return services;
        }
    }
}
