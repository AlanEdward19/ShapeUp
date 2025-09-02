using AuthService.Connections.Database;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            .ConfigureSqlServer(configuration)
            .ConfigureFirebase(configuration);

        return services;
    }
    
    private static IServiceCollection ConfigureFirebase(this IServiceCollection services, IConfiguration configuration)
    {
        var credsJson = configuration["Firebase:Credentials"];
        
        if (string.IsNullOrWhiteSpace(credsJson))
        {
            var section = configuration.GetSection("Firebase:Credentials");
            if (section.GetChildren().Any())
            {
                var dict = section.GetChildren().ToDictionary(c => c.Key, c => c.Value);
                credsJson = System.Text.Json.JsonSerializer.Serialize(dict);
            }
        }

        if (string.IsNullOrWhiteSpace(credsJson))
            throw new InvalidOperationException("Firebase:Credentials não encontrado ou vazio.");

        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(credsJson)
            });
        }

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
    }
}