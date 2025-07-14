using Domain.Repositories;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<PersistenceContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Base"),
                    sqlServerOptionsAction =>
                    {
                        sqlServerOptionsAction.MigrationsHistoryTable("__MicroMigrationHistory", configuration.GetConnectionString("BaseSchema"));
                    });

                options.ConfigureWarnings(warnings =>
                {
                    warnings.Default(WarningBehavior.Log);
                });
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
