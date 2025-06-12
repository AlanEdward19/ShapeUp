using MongoDB.Driver;

namespace NutritionService.Connections;

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
            .ConfigureMongoDb(configuration);

        return services;
    }

    private static IServiceCollection ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoDatabase>(x =>
        {
            var connectionString = configuration.GetConnectionString("Mongo")!;
            
            var client = new MongoClient(connectionString);
            return client.GetDatabase("nutrition");
        });
        services.AddScoped<NutritionDbContext>();
        return services;
    }
}