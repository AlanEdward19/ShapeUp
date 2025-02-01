using SocialService.ActivityFeed;
using SocialService.Common.Services;
using SocialService.Connections;
using SocialService.Follow;
using SocialService.Friends;
using SocialService.Post;
using SocialService.Profile;
using SocialService.Recommendation;

namespace SocialService.Configuration;

/// <summary>
///     Classe para resolver as dependências de serviços
/// </summary>
public static class ServiceDependencies
{
    /// <summary>
    ///     Método para resolver as dependências de serviços
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection SolveServiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .ConfigureCommonRelatedDependencies()
            .ConfigureConnections(configuration)
            .ConfigureActivityFeedRelatedDependencies()
            .ConfigureProfileRelatedDependencies()
            .ConfigureFollowRelatedDependencies()
            .ConfigureFriendsRelatedDependencies()
            .ConfigurePostRelatedDependencies()
            .ConfigureRecomendationRelatedDependencies();

        return services;
    }
}