﻿using SocialService.ActivityFeed.GetActivityFeed;

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
    public async Task<IEnumerable<Post.Post>> BuildActivityFeed(GetActivityFeedQuery query, Guid profileId)
    {
        var cypherQuery = $@"
    MATCH (profile:Profile {{id: '{profileId}'}})-[:FOLLOWING|FRIEND]->(friend:Profile)-[:PUBLISHED_BY]->(post:Post)
    RETURN post, friend.id AS publisherId, friend.firstName as publisherFirstName, friend.lastName as publisherLastName, friend.imageUrl as publisherImageUrl
    ORDER BY rand()
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
            post.MapToEntityFromNeo4j(parsedDictionary);
            return post;
        }).ToList();

        return posts;
    }
}