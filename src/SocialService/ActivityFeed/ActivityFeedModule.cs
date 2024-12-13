using SocialService.ActivityFeed.Common.Repository;
using SocialService.ActivityFeed.GetActivityFeed;
using SocialService.Common.Interfaces;

namespace SocialService.ActivityFeed;

/// <summary>
///     Modulo para resolver as dependências relacionadas a feed de atividades
/// </summary>
public static class ActivityFeedModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a feed de atividades
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureActivityFeedRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<IEnumerable<Post.Post>, GetActivityFeedQuery>, GetActivityFeedQueryHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IActivityFeedRepository, ActivityFeedRepository>();

        return services;
    }
}