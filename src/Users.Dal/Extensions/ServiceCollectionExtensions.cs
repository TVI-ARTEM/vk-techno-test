﻿using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.NameTranslation;
using Users.Bll.Models;
using Users.Bll.Repositories.Interfaces;
using Users.Dal.Contexts;
using Users.Dal.Repositories;
using Users.Dal.Settings;

namespace Users.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.Configure<DalOptions>(configuration.GetSection(nameof(DalOptions)));

        MapTypes();

        services.AddMigrations();

        services.AddDbContext<UserContext>((s, options) =>
        {
            var cfg = s.GetRequiredService<IOptions<DalOptions>>();
            options.UseLazyLoadingProxies().UseNpgsql(cfg.Value.ConnectionString);
        });

        services.AddRepositories();


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


    private static void MapTypes()
    {
        var mapper = NpgsqlConnection.GlobalTypeMapper;

        mapper.MapEnum<UserGroupEnum>("user_group_enum", Translator);
        mapper.MapEnum<UserStateEnum>("user_state_enum", Translator);
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}