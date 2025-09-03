using TrainingService.Connections.Database;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace TrainingService.Connections;

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
            .ConfigureMongoDb(configuration)
            .ConfigureSqlServer(configuration);

        return services;
    }

    private static IServiceCollection ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlServer")!;

#if (DEBUG)
        services.AddDbContext<TrainingDbContext>(options =>
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
        services.AddDbContext<TrainingDbContext>(options =>
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
    
    private static IServiceCollection ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoDatabase>(x =>
        {
            string connectionString = configuration.GetConnectionString("Mongo")!;

            var client = new MongoClient(connectionString);
            return client.GetDatabase("workoutSessions");
        });

        return services;
    }
}