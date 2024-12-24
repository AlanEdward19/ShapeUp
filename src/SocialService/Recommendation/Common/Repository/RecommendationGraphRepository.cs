using SocialService.Profile;
using SocialService.Recommendation.GetFriendRecommendations;

namespace SocialService.Recommendation.Common.Repository;

/// <summary>
///     Repositório de recomendações de amigos.
/// </summary>
/// <param name="graphContext"></param>
public class RecommendationGraphRepository(GraphContext graphContext) : IRecommendationGraphRepository
{
    /// <summary>
    ///     Método para obter recomendações de amigos.
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FriendRecommendation>> GetFriendRecommendationsAsync(Guid profileId)
    {
        var cypherQuery = $@"
    MATCH (profile:Profile {{id: '{profileId}'}})-[:FRIEND]->(friend:Profile)-[:FRIEND]->(fof:Profile)
    WHERE NOT (profile)-[:FRIEND]->(fof) AND profile <> fof
    WITH fof, COUNT(friend) AS mutualFriends
    RETURN fof, mutualFriends
    ORDER BY mutualFriends DESC";

        var result = await graphContext.ExecuteQueryAsync(cypherQuery);

        var recommendations = result.Select(record =>
        {
            var profile = new Profile.Profile();
            var parsedDictionary = record["fof"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            profile.MapToEntityFromNeo4j(parsedDictionary);
            var mutualFriends = record["mutualFriends"].As<int>();

            ProfileDto profileDto = new(profile);
            return new FriendRecommendation(profileDto, mutualFriends);
        }).ToList();

        return recommendations;
    }
}