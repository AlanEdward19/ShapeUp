using SocialService.Connections.Search;
using StackExchange.Redis;

namespace SocialService.Connections;

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
            .ConfigureNeo4J(configuration)
            .ConfigureRedis(configuration)
            .ConfigureStorageProvider(configuration)
            .ConfigureSearchProvider(configuration);

        return services;
    }
    
    private static IServiceCollection ConfigureNeo4J(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDriver>(_ =>
        {
            var uri = configuration["Neo4j:Uri"];
            var user = configuration["Neo4j:User"];
            var password = configuration["Neo4j:Password"];
            return GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        });

        services.AddScoped<GraphContext>();

        return services;
    }

    private static IServiceCollection ConfigureStorageProvider(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IBlobStorageProvider>(provider =>
        {
            var connectionString = configuration.GetConnectionString("BlobStorage")!;

            return new BlobStorageProvider(connectionString, provider.GetService<ILogger<BlobStorageProvider>>()!);
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
    
    private static IServiceCollection ConfigureSearchProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAzureSearchProvider>(x => new AzureSearchProvider(configuration));

        return services;
    }
}