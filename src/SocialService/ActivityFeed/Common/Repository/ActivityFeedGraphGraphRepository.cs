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
        var cypherQuery = $@"
    MATCH (profile:Profile {{id: '{profileId}'}})
    MATCH (friend:Profile)-[:PUBLISHED_BY]->(post:Post)
    WHERE ((post.visibility = 'Public' AND (profile)-[:FOLLOWING|FRIEND]->(friend)) OR
           (post.visibility = 'FriendsOnly' AND (profile)-[:FRIEND]->(friend)) OR
           (post.visibility = 'Private' AND friend.id = '{profileId}') OR
           ((post.visibility = 'Public' OR post.visibility = 'FriendsOnly') AND friend.id = '{profileId}'))

    CALL {{
        WITH post
        MATCH (post)<-[:COMMENTED_ON]-(comment:Comment)
        RETURN COUNT(comment) AS commentsCount
    }}

    CALL {{
        WITH post
        MATCH (post)<-[:REACTED]-(reaction)
        RETURN COUNT(reaction) AS reactionsCount
    }}

    CALL {{
        WITH post
        MATCH (post)<-[r:REACTED]-()
        RETURN r.type AS reactionType, COUNT(r) AS count
        ORDER BY count DESC
        LIMIT 3
    }}
    WITH post, friend, commentsCount, reactionsCount, COLLECT(reactionType) AS topReactions

    RETURN post,
           friend.id AS publisherId,
           friend.firstName AS publisherFirstName,
           friend.lastName AS publisherLastName,
           friend.imageUrl AS publisherImageUrl,
           commentsCount,
           reactionsCount,
           topReactions
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
            post.MapToEntityFromNeo4j(parsedDictionary);
            return post;
        }).ToList();

        return posts;
    }
}