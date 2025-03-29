using AuthService.Common.Filters;
using AuthService.Common.User.Repository;

namespace AuthService.Common;

public static class CommonModule
{
    public static IServiceCollection ConfigureCommonRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}