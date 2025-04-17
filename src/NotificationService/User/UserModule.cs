
using NotificationService.Common.Interfaces;
using NotificationService.User.Repository;
using NotificationService.User.UserLoggedIn;

namespace NotificationService.User;

public static class UserModule
{
    public static IServiceCollection ConfigureUserRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<bool, UserLoggedInCommand>, UserLoggedInCommandHandler>();
        
        return services;
    }
}