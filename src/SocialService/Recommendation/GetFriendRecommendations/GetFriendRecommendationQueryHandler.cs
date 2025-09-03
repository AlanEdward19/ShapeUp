using SharedKernel.Utils;
using SocialService.Profile;
using SocialService.Recommendation.Common.Repository;

namespace SocialService.Recommendation.GetFriendRecommendations;

/// <summary>
///     Handler para a query de recomendações de amigos.
/// </summary>
/// <param name="repository"></param>
public class GetFriendRecommendationQueryHandler(IRecommendationGraphRepository repository, IBlobStorageProvider blobStorageProvider)
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
        {
            ProfileSimplifiedDto profileSimplified = new(profile);
            
            if (!string.IsNullOrWhiteSpace(profileSimplified.ImageUrl))
                profileSimplified.SetImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(profileSimplified.ImageUrl, $"{profileSimplified.Id}"));
            
            result.Add(new FriendRecommendation(profileSimplified, mutualFriends));
            
        }

        foreach (var profile in recommendationsBaseadOnDistance)
        {
            ProfileSimplifiedDto profileSimplified = new(profile);
            
            if (!string.IsNullOrWhiteSpace(profileSimplified.ImageUrl))
                profileSimplified.SetImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(profileSimplified.ImageUrl, $"{profileSimplified.Id}"));
            
            result.Add(new FriendRecommendation(profileSimplified, 0));
        }
        
        result = result.GroupBy(x => x.Profile.Id)
            .Select(g => g.OrderByDescending(x => x.MutualFriends).First())
            .OrderByDescending(x => x.MutualFriends)
            .ToList();

        return result;
    }
}