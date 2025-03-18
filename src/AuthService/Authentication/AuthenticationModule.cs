using AuthService.Authentication.AuthenticateUser;
using AuthService.Authentication.Common.Services.AzureAd;
using AuthService.Authentication.Common.Services.Token;
using AuthService.Common.Interfaces;

namespace AuthService.Authentication;

public static class AuthenticationModule
{
    public static IServiceCollection ConfigureAuthenticationRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddHandlers()
            .AddServices();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<string, AuthenticateUserCommand>, AuthenticateUserCommandHandler>();
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAzureAdService, AzureAdService>();
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}