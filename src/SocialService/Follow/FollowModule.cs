using SocialService.Follow.Common.Repository;
using SocialService.Follow.FollowUser;
using SocialService.Follow.GetFollowers;
using SocialService.Follow.GetFollowing;
using SocialService.Follow.UnfollowUser;

namespace SocialService.Follow;

/// <summary>
///     Modulo para resolver as dependências relacionadas a seguidores
/// </summary>
public static class FollowModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a seguidores
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureFollowRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<bool, FollowUserCommand>, FollowUserCommandHandler>();
        services
            .AddScoped<IHandler<IEnumerable<ProfileBasicInformation>, GetFollowersQuery>,
                GetFollowersQueryHandler>();
        services
            .AddScoped<IHandler<IEnumerable<ProfileBasicInformation>, GetFollowingQuery>,
                GetFollowingQueryHandler>();
        services.AddScoped<IHandler<bool, UnfollowUserCommand>, UnfollowUserCommandHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFollowerGraphRepository, FollowerGraphRepository>();

        return services;
    }
}