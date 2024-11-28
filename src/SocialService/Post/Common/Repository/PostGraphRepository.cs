using Neo4j.Driver;
using SocialService.Database.Graph;
using SocialService.Post.CommentsOnPost;
using SocialService.Post.Common.Enums;
using SocialService.Post.CreatePost;
using SocialService.Post.GetPostComments;

namespace SocialService.Post.Common.Repository;

/// <summary>
/// Repositorio de grafo para post
/// </summary>
/// <param name="graphContext"></param>
public class PostGraphRepository(GraphContext graphContext) : IPostGraphRepository
{
    
    /// <summary>
    /// Método que verifica se um post existe
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="profileId"></param>
    /// <returns></returns>
    public async Task<bool> PostExistsAsync(Guid postId, Guid profileId)
    {
        var query = $@"
        MATCH (p:Profile {{id: '{profileId}'}})-[:PUBLISHED_BY]->(post:Post {{id: '{postId}'}})
        RETURN post";

        var result = await graphContext.ExecuteQueryAsync(query);
        return (result ?? Array.Empty<IRecord>()).Any();
    }

    /// <summary>
    /// Método que cria um post
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="postId"></param>
    /// <param name="command"></param>
    public async Task CreatePostAsync(Guid profileId, Guid postId, CreatePostCommand command)
    {
        var query = $@"
        MATCH (p:Profile {{id: '{profileId}'}})
        CREATE (post:Post {{
            id: '{postId}',
            content: '{command.Content}',
            images: null,
            createdAt: datetime('{DateTime.Now:yyyy-MM-dd}'),
            visibility: '{command.Visibility}'
        }})
        CREATE (p)-[:PUBLISHED_BY]->(post)";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    /// Método que adiciona imagens a um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="images"></param>
    public async Task UploadPostImagesAsync(Guid postId, List<Guid> images)
    {
        var imagesString = string.Join(",", images.Select(id => $"'{id}'"));
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})
    SET post.images = [{imagesString}]
    RETURN post";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    /// Método que deleta um post
    /// </summary>
    /// <param name="postId"></param>
    public async Task DeletePostAsync(Guid postId)
    {
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})
    OPTIONAL MATCH (post)<-[:COMMENTED_ON]-(comment:Comment)
    OPTIONAL MATCH (post)<-[r:REACTED]-()
    DETACH DELETE post, comment, r";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    /// Método que atualiza um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="command"></param>
    public async Task UpdatePostAsync(EditPostCommand command)
    {
        List<string> @params = new();
        if (command.Visibility != null)
            @params.Add($"SET post.visibility = '{command.Visibility}'");

        if (command.Content != null)
            @params.Add($"SET post.content = '{command.Content}'");

        var query = $@"
        MATCH (post:Post {{id: '{command.PostId}'}})
        {string.Join(",", @params)}
        RETURN post";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    /// Método que comenta em um post
    /// </summary>
    /// <param name="command"></param>
    /// <param name="profileId"></param>
    public async Task CommentOnPostAsync(CommentOnPostCommand command, Guid profileId)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{profileId}'}})
    MATCH (post:Post {{id: '{command.PostId}'}})
    CREATE (p)-[:COMMENTED]->(comment:Comment {{
        id: '{Guid.NewGuid()}',
        content: '{command.Content}',
        createdAt: datetime('{DateTime.Now:yyyy-MM-dd}')
    }})-[:COMMENTED_ON]->(post)";

        await graphContext.ExecuteQueryAsync(query);
    }

    public async Task<IEnumerable<Comment>> GetPostCommentsAsync(Guid postId)
    {
        var comments = new List<Comment>();
        var cypherQuery = $@"
    MATCH (post:Post {{id: '{postId}'}})<-[:COMMENTED_ON]-(comment:Comment)<-[:COMMENTED]-(profile:Profile)
    RETURN comment, profile.id AS profileId";

        var result = await graphContext.ExecuteQueryAsync(cypherQuery);

        foreach (var record in result)
        {
            var comment = new Comment();

            var parsedDictionary =
                record["comment"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedDictionary.Add("profileId", record["profileId"].ToString()!);
            comment.MapToEntityFromNeo4j(parsedDictionary);

            comments.Add(comment);
        }

        return comments;
    }

    /// <summary>
    /// Método que cria uma reação em um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="profileId"></param>
    /// <param name="reactionType"></param>
    public async Task LikePostAsync(Guid postId, Guid profileId, EReactionType reactionType)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{profileId}'}})
    MATCH (post:Post {{id: '{postId}'}})
    MERGE (p)-[r:REACTED]->(post)
    SET r.type = '{reactionType}'
    RETURN r";

        await graphContext.ExecuteQueryAsync(query);
    }
}