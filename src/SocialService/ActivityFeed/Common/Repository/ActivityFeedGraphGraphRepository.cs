using SocialService.ActivityFeed.GetActivityFeed;

namespace SocialService.ActivityFeed.Common.Repository;

/// <summary>
///     Repositório de feed de atividades.
/// </summary>
/// <param name="graphContext"></param>
public class ActivityFeedGraphGraphRepository(GraphContext graphContext) : IActivityFeedGraphRepository
{
    /// <summary>
    ///     Método para construir o feed de atividades.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="profileId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Post.Post>> BuildActivityFeed(GetActivityFeedQuery query, string profileId)
    {
        string cypherQuery = $@"
MATCH (profile:Profile {{id: '{profileId}'}})
MATCH (friend:Profile)-[:PUBLISHED_BY]->(post:Post)
WHERE
(
    (profile)-[:FOLLOWING]->(friend) AND
    (
        post.visibility = 'Public' OR
        (post.visibility = 'FriendsOnly' AND (profile)-[:FRIEND]->(friend))
    )
)
OR
(friend.id = profile.id AND post.visibility IN ['Public', 'FriendsOnly', 'Private'])

CALL {{
    WITH post
    OPTIONAL MATCH (post)<-[:COMMENTED_ON]-(comment:Comment)
    RETURN COUNT(comment) AS commentsCount
}}

CALL {{
    WITH post
    OPTIONAL MATCH (post)<-[r:REACTED]-(:Profile)
    RETURN COUNT(r) AS reactionsCount
}}

CALL {{
    WITH post
    OPTIONAL MATCH (post)<-[r:REACTED]-()
    RETURN COLLECT({{type: r.type, count: 1}}) AS rawReactions
}}

CALL {{
    WITH post, profile
    OPTIONAL MATCH (post)<-[r:REACTED]-(profile)
    RETURN COUNT(r) > 0 AS hasUserReacted
}}

CALL {{
    WITH post, profile
    OPTIONAL MATCH (post)<-[:COMMENTED_ON]-(c:Comment)-[:COMMENTED]->(profile)
    RETURN COUNT(c) > 0 AS hasUserCommented
}}

WITH post, friend, commentsCount, reactionsCount, rawReactions, hasUserReacted, hasUserCommented

UNWIND rawReactions AS r
WITH post, friend, commentsCount, reactionsCount, hasUserReacted, hasUserCommented, r.type AS reactionType
WITH post, friend, commentsCount, reactionsCount, hasUserReacted, hasUserCommented, reactionType, COUNT(*) AS count
ORDER BY count DESC
WITH post, friend, commentsCount, reactionsCount, hasUserReacted, hasUserCommented, COLLECT(reactionType)[0..3] AS topReactions

RETURN post,
       friend.id AS publisherId,
       friend.firstName AS publisherFirstName,
       friend.lastName AS publisherLastName,
       friend.imageUrl AS publisherImageUrl,
       commentsCount,
       reactionsCount,
       topReactions,
       hasUserReacted,
       hasUserCommented
ORDER BY post.creationDate DESC, rand()
SKIP {(query.Page - 1) * query.Rows}
LIMIT {query.Rows}";

        var result = await graphContext.ExecuteQueryAsync(cypherQuery);

        var posts = result.Select(record =>
        {
            var post = new Post.Post();
            var parsedDictionary = record["post"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedDictionary.Add("publisherId", record["publisherId"].ToString());
            parsedDictionary.Add("publisherFirstName", record["publisherFirstName"].ToString());
            parsedDictionary.Add("publisherLastName", record["publisherLastName"].ToString());
            parsedDictionary.Add("publisherImageUrl", record["publisherImageUrl"].ToString());
            parsedDictionary.Add("commentsCount", record["commentsCount"].ToString());
            parsedDictionary.Add("reactionsCount", record["reactionsCount"].ToString());
            parsedDictionary.Add("topReactions", record["topReactions"].As<List<object>>().Select(r => r.ToString()));
            parsedDictionary.Add("hasUserReacted", record["hasUserReacted"].ToString());
            parsedDictionary.Add("hasUserCommented", record["hasUserCommented"].ToString());
            post.MapToEntityFromNeo4j(parsedDictionary);
            return post;
        }).ToList();

        return posts;
    }
}