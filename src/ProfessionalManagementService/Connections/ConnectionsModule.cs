using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Connections.Database;

namespace ProfessionalManagementService.Connections;

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
        string connectionString = configuration.GetConnectionString("SqlServerProfessionalManagement")!;

#if (DEBUG)
        services.AddDbContext<DatabaseContext>(options =>
            options
                .UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );
                })
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                // ⚠️ !! Ativado somente em modo debug !! ⚠️ 
                .EnableSensitiveDataLogging()
        );

#elif (RELEASE)
        services.AddDbContext<DatabaseContext>(options =>
               options
               .UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );
                })
               .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
       );
#endif

        return services;
    }
}