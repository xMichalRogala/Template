using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Core.Abstractions.Data;
using Template.Domain.Repositories;
using Template.Persistance.Infrastructure;
using Template.Persistance.Repositories;

namespace Template.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentEnvironment)
        {
            string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey)
                ?? throw new ArgumentNullException($"There is no connection string with key: {ConnectionString.SettingsKey}");

            var connectionStringObj = new ConnectionString(connectionString);

            services.AddSingleton(connectionStringObj);
            services.AddDbContext<CoreDbContext>(options =>
            {
                options.UseSqlServer(connectionStringObj);

                if (isDevelopmentEnvironment)
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CoreDbContext>());
            services.AddScoped(typeof(IConcreteGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IQueryableGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
