using SocialService.Recommendation.Common.Repository;
using SocialService.Recommendation.GetFriendRecommendations;

namespace SocialService.Recommendation;

/// <summary>
///     Modulo para resolver as dependências relacionadas a postagens
/// </summary>
public static class RecomendationModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a recomendações
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureRecomendationRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services
            .AddScoped<IHandler<IEnumerable<FriendRecommendation>, GetFriendRecommendationQuery>,
                GetFriendRecommendationQueryHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRecommendationGraphRepository, RecommendationGraphRepository>();

        return services;
    }
}