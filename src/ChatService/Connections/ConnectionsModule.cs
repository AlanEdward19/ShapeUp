using MongoDB.Driver;
using StackExchange.Redis;

namespace ChatService.Connections;

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
            .ConfigureRedis(configuration)
            .AddSignalR();

        return services;
    }

    private static IServiceCollection ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoDatabase>(x =>
        {
            string connectionString = configuration.GetConnectionString("Mongo")!;

            var client = new MongoClient(connectionString);
            return client.GetDatabase("chatMessages");
        });

        return services;
    }
    
    private static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Redis")!;
        
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(connectionString));

        return services;
    }
}