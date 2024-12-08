using Neo4j.Driver;
using SocialService.Database.Graph;
using SocialService.Post.CommentsOnPost;
using SocialService.Post.Common.Enums;
using SocialService.Post.CreatePost;
using SocialService.Post.EditCommentOnPost;
using SocialService.Post.GetPostComments;
using SocialService.Post.GetReactionsOnPost;

namespace SocialService.Post.Common.Repository;

/// <summary>
/// Repositorio de grafo para post
/// </summary>
/// <param name="graphContext"></param>
public class PostGraphRepository(GraphContext graphContext) : IPostGraphRepository
{
    #region Post
    
    /// <summary>
    /// Método que retorna um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public async Task<Post> GetPostAsync(Guid postId)
    {
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})
    RETURN post";

        var result = await graphContext.ExecuteQueryAsync(query);
        var record = result.FirstOrDefault();

        if (record == null)
            return null;

        var post = new Post();
        var parsedDictionary = record["post"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        post.MapToEntityFromNeo4j(parsedDictionary);

        return post;
    }

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
    public async Task<Post> CreatePostAsync(Guid profileId, Guid postId, CreatePostCommand command)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{profileId}'}})
    CREATE (post:Post {{
        id: '{postId}',
        content: '{command.Content}',
        images: null,
        createdAt: datetime('{DateTime.Now:yyyy-MM-dd}'),
        updatedAt: datetime('{DateTime.Now:yyyy-MM-dd}'),
        visibility: '{command.Visibility}'
    }})
    CREATE (p)-[:PUBLISHED_BY]->(post)
    RETURN post";

        var result = await graphContext.ExecuteQueryAsync(query);
        var record = result.FirstOrDefault();

        if (record == null)
            return null;

        var post = new Post();
        var parsedDictionary = record["post"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        post.MapToEntityFromNeo4j(parsedDictionary);

        return post;
    }

    /// <summary>
    /// Método que atualiza um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="command"></param>
    public async Task<Post> UpdatePostAsync(EditPostCommand command)
    {
        List<string> @params = new();

        if (command.Visibility != null)
            @params.Add($"post.visibility = '{command.Visibility}'");

        if (command.Content != null)
            @params.Add($"post.content = '{command.Content}'");

        @params.Add("post.updatedAt = datetime()");

        var query = $@"
    MATCH (post:Post {{id: '{command.PostId}'}})
    SET {string.Join(", ", @params)}
    RETURN post";

        var result = await graphContext.ExecuteQueryAsync(query);
        var record = result.FirstOrDefault();

        if (record == null)
            return null;

        var post = new Post();
        var parsedDictionary = record["post"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        post.MapToEntityFromNeo4j(parsedDictionary);

        return post;
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

    #endregion

    #region Comment

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
    /// Método que edita um comentário
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<Comment> EditCommentOnPostAsync(EditCommentOnPostCommand command)
    {
        var query = $@"
    MATCH (comment:Comment {{id: '{command.CommentId}'}})<-[:COMMENTED]-(profile:Profile)
    SET comment.content = '{command.Content}'
    RETURN comment, profile.id AS profileId";

        var result = await graphContext.ExecuteQueryAsync(query);
        var record = result.FirstOrDefault();

        if (record == null)
            return null;

        var comment = new Comment();
        var parsedDictionary = record["comment"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        parsedDictionary.Add("profileId", record["profileId"].ToString()!);
        comment.MapToEntityFromNeo4j(parsedDictionary);

        return comment;
    }

    /// <summary>
    /// Método que deleta um comentário
    /// </summary>
    /// <param name="commentId"></param>
    public async Task DeleteCommentOnPostAsync(Guid commentId)
    {
        var query = $@"
    MATCH (comment:Comment {{id: '{commentId}'}})
    OPTIONAL MATCH (comment)-[r]-()
    DELETE comment, r";

        await graphContext.ExecuteQueryAsync(query);
    }

    #endregion

    #region React

    /// <summary>
    /// Método que cria/atualiza se ja existir uma reação em um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="profileId"></param>
    /// <param name="reactionType"></param>
    public async Task ReactToPostAsync(Guid postId, Guid profileId, EReactionType reactionType)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{profileId}'}})
    MATCH (post:Post {{id: '{postId}'}})
    MERGE (p)-[r:REACTED]->(post)
    ON CREATE SET r.id = '{Guid.NewGuid()}', r.createdAt = datetime()
    ON MATCH SET r.type = '{reactionType}', r.createdAt = datetime()
    RETURN r";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    /// Método que retorna as reações de um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Reaction>> GetReactionsOnPostAsync(Guid postId)
    {
        var reactions = new List<Reaction>();
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})<-[r:REACTED]-(profile:Profile)
    RETURN r, profile.id AS profileId";

        var result = await graphContext.ExecuteQueryAsync(query);

        foreach (var record in result)
        {
            var reaction = new Reaction();
            var parsedDictionary = record["r"].As<IRelationship>().Properties
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedDictionary.Add("profileId", record["profileId"].ToString()!);
            reaction.MapToEntityFromNeo4j(parsedDictionary);

            reactions.Add(reaction);
        }

        return reactions;
    }

    /// <summary>
    /// Método que deleta uma reação em um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="profileId"></param>
    public async Task DeleteReactionOnPostAsync(Guid postId, Guid profileId)
    {
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})<-[r:REACTED]-(profile:Profile {{id: '{profileId}'}})
    DELETE r";

        await graphContext.ExecuteQueryAsync(query);
    }

    #endregion
}