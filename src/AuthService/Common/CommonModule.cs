using System.Security.Cryptography.X509Certificates;
using System.Text;
using AuthService.Common.User.Repository;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Common;

public static class CommonModule
{
    public static IServiceCollection ConfigureCommonRelatedDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRepositories();
        
        GetIssuerSigningKey(configuration);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
    
    private static void GetIssuerSigningKey(IConfiguration configuration)
    {
        var httpClient = new HttpClient();
        var response = httpClient.GetStringAsync(configuration["Firebase:IssuerSigning"]).GetAwaiter().GetResult();
        
        var certificates = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(response);

        configuration["Firebase:IssuerSigningKey"] = string.Join("-?-", certificates.Values);
    }
}