﻿using AuthService.Connections.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Connections;

/// <summary>
///     Modulo de conexões externas
/// </summary>
public static class ConnectionsModule
{
    /// <summary>
    ///     Método para configurar as conexões
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureConnections(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .ConfigureSqlServer(configuration);

        return services;
    }

    private static IServiceCollection ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlServer")!;

#if (DEBUG)
        services.AddDbContext<AuthDbContext>(options =>
            options
                .UseSqlServer(connectionString)
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                // ⚠️ !! Ativado somente em modo debug !! ⚠️ 
                .EnableSensitiveDataLogging()
        );

#elif (RELEASE)
        services.AddDbContext<AuthDbContext>(options =>
               options
               .UseSqlServer(connectionString)
               .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
       );
#endif

        return services;

        return services;
    }
}