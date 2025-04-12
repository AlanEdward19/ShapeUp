using AuthService.Authentication.EnhanceToken;
using AuthService.Common.Interfaces;

namespace AuthService.Authentication;

public static class AuthenticationModule
{
    public static IServiceCollection ConfigureAuthenticationRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddHandlers();

        return services;
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<bool, EnhanceTokenCommand>, EnhanceTokenCommandHandler>();
        
        return services;
    }
}