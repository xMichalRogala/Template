﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Core.Abstractions.Auth;
using Template.Application.Core.Abstractions.Commons;
using Template.Infrastructure.Core.Auth;
using Template.Infrastructure.Core.Commons;

namespace Template.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
