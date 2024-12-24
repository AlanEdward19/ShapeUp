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
            .ConfigureStorageProvider(configuration);

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

        services.AddScoped(typeof(GraphContext));

        return services;
    }

    private static IServiceCollection ConfigureStorageProvider(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IStorageProvider>(provider =>
        {
            var connectionString = configuration.GetConnectionString("StorageConnection")!;

            return new StorageProvider(connectionString, provider.GetService<ILogger<StorageProvider>>()!);
        });

        return services;
    }
}