using NotificationService.Notification.Common;
using StackExchange.Redis;

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
            .ConfigureRedis(configuration);

        return services;
    }
    
    private static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Redis")!;
        
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(connectionString));

        return services;
    }
    
    private static IServiceCollection ConfigureSignalR(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }
}