using AuthService.Connections.Database;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedKernel.Providers.Grpc;

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
            .ConfigureGrpc()
            .ConfigureFirebase(configuration);

        return services;
    }
    
    private static IServiceCollection ConfigureFirebase(this IServiceCollection services, IConfiguration configuration)
    {
        var firebaseCredentials = configuration.GetSection("Firebase:Credentials").GetChildren()
            .ToDictionary(x => x.Key, x => x.Value);

        var jsonCredentials = JsonConvert.SerializeObject(firebaseCredentials);

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromJson(jsonCredentials)
        });

        return services;
    }
    
    private static IServiceCollection ConfigureGrpc(this IServiceCollection services)
    {
        services.AddScoped<IGrpcProvider, GrpcProvider>();

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
    
    public static IEndpointRouteBuilder ConfigureGrpc(this IEndpointRouteBuilder builder)
    {
        builder.MapGrpcService<Services.AuthService>();

        return builder;
    }
}