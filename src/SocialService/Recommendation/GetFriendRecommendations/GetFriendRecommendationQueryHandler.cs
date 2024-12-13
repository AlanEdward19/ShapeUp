using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Recommendation.Common.Repository;

namespace SocialService.Recommendation.GetFriendRecommendations;

/// <summary>
/// Handler para a query de recomendações de amigos.
/// </summary>
/// <param name="repository"></param>
public class GetFriendRecommendationQueryHandler(IRecommendationGraphRepository repository)
    : IHandler<IEnumerable<FriendRecommendation>, GetFriendRecommendationQuery>
{
    /// <summary>
    /// Método para obter recomendações de amigos.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FriendRecommendation>> HandleAsync(GetFriendRecommendationQuery query,
        CancellationToken cancellationToken)
    {
        return await repository.GetFriendRecommendationsAsync(ProfileContext.ProfileId);
    }
}