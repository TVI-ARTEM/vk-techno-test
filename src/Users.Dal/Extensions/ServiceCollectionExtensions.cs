using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Users.Dal.Settings;

namespace Users.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.Configure<DalOptions>(configuration.GetSection(nameof(DalOptions)));

        services.AddMigrations();

        return services;
    }

    private static IServiceCollection AddMigrations(this IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                {
                    var cfg = s.GetRequiredService<IOptions<DalOptions>>();
                    return cfg.Value.ConnectionString;
                })
                .ScanIn(typeof(ServiceCollectionExtensions).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        return services;
    }
}