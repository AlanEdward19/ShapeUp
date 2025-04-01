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
    Task<IEnumerable<(Profile.Profile profile, int mutualFriends)>> GetFriendRecommendationsAsync(string profileId);
    
    /// <summary>
    ///    Método para obter recomendações de amigos dentro de uma distância.
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="distanceInKm"></param>
    /// <returns></returns>
    Task<IEnumerable<Profile.Profile>> GetFriendRecommendationsWithinDistanceAsync(string profileId, double distanceInKm);
}