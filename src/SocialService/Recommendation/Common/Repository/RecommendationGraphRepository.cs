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
    public async Task<IEnumerable<(Profile.Profile profile, int mutualFriends)>> GetFriendRecommendationsAsync(string profileId)
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

            return (profile, mutualFriends);
        }).ToList();

        return recommendations;
    }

    /// <summary>
    ///    Método para obter recomendações de amigos dentro de uma distância.
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="distanceInKm"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Profile.Profile>> GetFriendRecommendationsWithinDistanceAsync(string profileId, double distanceInKm)
    {
        var cypherQuery = $@"
    MATCH (profile:Profile {{id: '{profileId}'}})
    MATCH (fof:Profile)
    WHERE NOT (profile)-[:FRIEND]->(fof) 
      AND NOT (fof)-[:FRIEND]->(profile) 
      AND NOT (profile)-[:FRIEND_REQUEST]->(fof) 
      AND NOT (fof)-[:FRIEND_REQUEST]->(profile) 
      AND profile <> fof
    WITH profile, fof,
         point({{latitude: profile.latitude, longitude: profile.longitude}}) AS profilePoint,
         point({{latitude: fof.latitude, longitude: fof.longitude}}) AS fofPoint
    WHERE point.distance(profilePoint, fofPoint) <= {distanceInKm * 1000}
    RETURN fof";

        var result = await graphContext.ExecuteQueryAsync(cypherQuery);

        var recommendations = result.Select(record =>
        {
            var profile = new Profile.Profile();
            var parsedDictionary = record["fof"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            profile.MapToEntityFromNeo4j(parsedDictionary);

            return profile;
        }).ToList();

        return recommendations;
    }
}