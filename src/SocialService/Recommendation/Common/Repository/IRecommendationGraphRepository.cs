using SocialService.Recommendation.GetFriendRecommendations;

namespace SocialService.Recommendation.Common.Repository;

/// <summary>
///     Interface para o repositório de grafo de recomendações.
/// </summary>
public interface IRecommendationGraphRepository
{
    /// <summary>
    ///     Método para obter recomendações de amigos.
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<IEnumerable<FriendRecommendation>> GetFriendRecommendationsAsync(Guid profileId);
}