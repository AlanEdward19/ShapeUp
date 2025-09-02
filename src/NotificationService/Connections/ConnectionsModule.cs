using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NotificationService.Connections.Database;
using NotificationService.Connections.Firebase;

namespace NotificationService.Connections;

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
            .ConfigureSignalR()
            .ConfigureMongoDb(configuration)
            .ConfigureFcm();

        return services;
    }

    private static IServiceCollection ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Mongo")!;
        
#if (DEBUG)
        services.AddDbContext<NotificationDbContext>(options =>
            options
                .UseMongoDB(connectionString, "NotificationDb")
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                // ⚠️ !! Ativado somente em modo debug !! ⚠️ 
                .EnableSensitiveDataLogging()
        );

#elif (RELEASE)
        services.AddDbContext<NotificationDbContext>(options =>
            options
                .UseMongoDB(connectionString, "NotificationDb")
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
        );
#endif

        return services;
    }

    private static IServiceCollection ConfigureFcm(this IServiceCollection services)
    {
        services.AddScoped<IFcmService, FcmService>();

        return services;
    }
    
    private static IServiceCollection ConfigureSignalR(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }
    
    public static IEndpointRouteBuilder ConfigureGrpc(this IEndpointRouteBuilder builder)
    {
        builder.MapGrpcService<Notification.Services.NotificationService>();

        return builder;
    }
}