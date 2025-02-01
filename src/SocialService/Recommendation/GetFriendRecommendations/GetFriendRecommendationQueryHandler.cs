using SocialService.Recommendation.Common.Repository;

namespace SocialService.Recommendation.GetFriendRecommendations;

/// <summary>
///     Handler para a query de recomendações de amigos.
/// </summary>
/// <param name="repository"></param>
public class GetFriendRecommendationQueryHandler(IRecommendationGraphRepository repository)
    : IHandler<IEnumerable<FriendRecommendation>, GetFriendRecommendationQuery>
{
    /// <summary>
    ///     Método para obter recomendações de amigos.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FriendRecommendation>> HandleAsync(GetFriendRecommendationQuery query,
        CancellationToken cancellationToken)
    {
        IEnumerable<(Profile.Profile, int)> recommendationsWithMutualFriends =
            await repository.GetFriendRecommendationsAsync(ProfileContext.ProfileId);

        IEnumerable<Profile.Profile> recommendationsBaseadOnDistance =
            await repository.GetFriendRecommendationsWithinDistanceAsync(ProfileContext.ProfileId, 15);
        
        List<FriendRecommendation> result = new(recommendationsBaseadOnDistance.Count() + recommendationsWithMutualFriends.Count());
        
        foreach (var (profile, mutualFriends) in recommendationsWithMutualFriends)
            result.Add(new FriendRecommendation(new(profile), mutualFriends));
        
        foreach (var profile in recommendationsBaseadOnDistance)
            result.Add(new FriendRecommendation(new(profile), 0));

        return result;
    }
}