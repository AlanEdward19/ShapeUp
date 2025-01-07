using SocialService.Post.React;

namespace SocialService.Post.Common.Repository;

/// <summary>
///     Repositorio de grafo para post
/// </summary>
/// <param name="graphContext"></param>
public class PostGraphRepository(GraphContext graphContext) : IPostGraphRepository
{
    #region Post

    /// <summary>
    ///     Método que retorna um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public async Task<Post> GetPostAsync(Guid postId)
    {
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})<-[:PUBLISHED_BY]-(profile:Profile)
    RETURN post, profile.id AS publisherId, profile.firstName as publisherFirstName, profile.lastName as publisherLastName, profile.ImageUrl as publisherImageUrl";

        var result = await graphContext.ExecuteQueryAsync(query);
        var record = result.FirstOrDefault();

        if (record == null)
            return null;

        var post = new Post();
        var parsedDictionary = record["post"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        parsedDictionary.Add("publisherId", record["publisherId"].ToString()!);
        parsedDictionary.Add("publisherFirstName", record["publisherFirstName"].ToString()!);
        parsedDictionary.Add("publisherLastName", record["publisherLastName"].ToString()!);
        parsedDictionary.Add("publisherImageUrl", (record["publisherImageUrl"] ?? "").ToString()!);
        post.MapToEntityFromNeo4j(parsedDictionary);

        return post;
    }

    /// <summary>
    /// Método que retorna os posts de um perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="requesterId"></param>
    /// <param name="page"></param>
    /// <param name="rows"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Post>> GetPostsByProfileIdAsync(Guid profileId, Guid requesterId, int page, int rows)
    {
        var skip = (page - 1) * rows;
        var cypherQuery = $@"
    MATCH (profile:Profile {{id: '{profileId}'}})
    MATCH (post:Post)<-[:PUBLISHED_BY]-(publisher:Profile)
    WHERE publisher.id = '{profileId}' AND
          ((post.visibility = 'Public') OR
           (post.visibility = 'FriendsOnly' AND (profile)-[:FRIEND]->(publisher)) OR
           (post.visibility = 'Private' AND publisher.id = '{requesterId}') OR
           ((post.visibility = 'Public' OR post.visibility = 'FriendsOnly') AND publisher.id = '{requesterId}'))
    RETURN post, publisher.id AS publisherId, publisher.firstName as publisherFirstName, publisher.lastName as publisherLastName, publisher.imageUrl as publisherImageUrl
    ORDER BY post.creationDate DESC
    SKIP {skip}
    LIMIT {rows}";

        var result = await graphContext.ExecuteQueryAsync(cypherQuery);

        var posts = result.Select(record =>
        {
            var post = new Post();
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

    /// <summary>
    ///     Método que verifica se um post existe
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public async Task<bool> PostExistsAsync(Guid postId)
    {
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})
    RETURN post";

        var result = await graphContext.ExecuteQueryAsync(query);
        return (result ?? Array.Empty<IRecord>()).Any();
    }

    /// <summary>
    ///     Método que cria um post
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="post"></param>
    public async Task CreatePostAsync(Guid profileId, Post post)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{profileId}'}})
    CREATE (post:Post {{
        id: '{post.Id}',
        content: '{post.Content}',
        images: null,
        createdAt: datetime('{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}'),
        updatedAt: datetime('{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}'),
        visibility: '{post.Visibility}'
    }})
    CREATE (p)-[:PUBLISHED_BY]->(post)";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método que atualiza um post
    /// </summary>
    /// <param name="post"></param>
    public async Task UpdatePostAsync(Post post)
    {
        var query = $@"
    MATCH (post:Post {{id: '{post.Id}'}})
    SET post.visibility = '{post.Visibility}', 
        post.content = '{post.Content}', 
        post.updatedAt = datetime('{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}')";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método que adiciona imagens a um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="images"></param>
    public async Task UploadPostImagesAsync(Guid postId, List<string> images)
    {
        var imagesString = string.Join(",", images.Select(url => $"'{url}'"));
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})
    SET post.images = [{imagesString}]
    RETURN post";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método que deleta um post
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
    ///     Método que comenta em um post
    /// </summary>
    /// <param name="command"></param>
    public async Task CommentOnPostAsync(Comment.Comment comment)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{comment.ProfileId}'}})
    MATCH (post:Post {{id: '{comment.PostId}'}})
    CREATE (p)-[:COMMENTED]->(comment:Comment {{
        id: '{comment.Id}',
        content: '{comment.Content}',
        createdAt: datetime('{DateTime.Now:yyyy-MM-dd}')
    }})-[:COMMENTED_ON]->(post)";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método que retorna um comentário de um post
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public async Task<Comment.Comment> GetPostCommentsByCommentIdAsync(Guid commentId)
    {
        var cypherQuery = $@"
    MATCH (comment:Comment {{id: '{commentId}'}})<-[:COMMENTED_ON]-(post:Post)
    RETURN comment, post.id AS postId";

        var result = await graphContext.ExecuteQueryAsync(cypherQuery);
        var record = result.FirstOrDefault();

        if (record == null)
            return null;

        var comment = new Comment.Comment();
        var parsedDictionary = record["comment"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        parsedDictionary.Add("postId", record["postId"].ToString()!);
        comment.MapToEntityFromNeo4j(parsedDictionary);

        return comment;
    }

    /// <summary>
    ///     Método que retorna os comentários de um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Comment.Comment>> GetPostCommentsByPostIdAsync(Guid postId)
    {
        var comments = new List<Comment.Comment>();
        var cypherQuery = $@"
    MATCH (post:Post {{id: '{postId}'}})<-[:COMMENTED_ON]-(comment:Comment)<-[:COMMENTED]-(profile:Profile)
    RETURN comment, profile.id AS profileId, profile.firstName as profileFirstName, profile.lastName as profileLastName, profile.imageUrl as profileImageUrl";

        var result = await graphContext.ExecuteQueryAsync(cypherQuery);

        foreach (var record in result)
        {
            var comment = new Comment.Comment();

            var parsedDictionary =
                record["comment"].As<INode>().Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedDictionary.Add("profileId", record["profileId"].ToString()!);
            parsedDictionary.Add("profileFirstName", record["profileFirstName"].ToString()!);
            parsedDictionary.Add("profileLastName", record["profileLastName"].ToString()!);
            parsedDictionary.Add("profileImageUrl", record["profileImageUrl"].ToString()!);

            comment.MapToEntityFromNeo4j(parsedDictionary);

            comments.Add(comment);
        }

        return comments;
    }

    /// <summary>
    ///     Método que edita um comentário
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    public async Task EditCommentOnPostAsync(Comment.Comment comment)
    {
        var query = $@"
    MATCH (comment:Comment {{id: '{comment.Id}'}})<-[:COMMENTED]-(profile:Profile)
    SET comment.content = '{comment.Content}'";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método que deleta um comentário
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

    /// <summary>
    ///     Método que verifica se um comentário existe
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> CommentExistsAsync(Guid commentId)
    {
        var query = $@"
    MATCH (comment:Comment {{id: '{commentId}'}})
    RETURN comment";

        var result = await graphContext.ExecuteQueryAsync(query);
        return (result ?? Array.Empty<IRecord>()).Any();
    }

    #endregion

    #region React

    /// <summary>
    ///     Método que cria/atualiza se ja existir uma reação em um post
    /// </summary>
    /// <param name="reaction"></param>
    public async Task ReactToPostAsync(Reaction reaction)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{reaction.ProfileId}'}})
    MATCH (post:Post {{id: '{reaction.PostId}'}})
    MERGE (p)-[r:REACTED]->(post)
    ON CREATE SET r.id = '{reaction.Id}', r.createdAt = datetime(), r.type = '{reaction.ReactionType}'
    ON MATCH SET r.type = '{reaction.ReactionType}', r.createdAt = datetime()
    RETURN r";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método que retorna as reações de um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Reaction>> GetReactionsOnPostAsync(Guid postId)
    {
        var reactions = new List<Reaction>();
        var query = $@"
    MATCH (post:Post {{id: '{postId}'}})<-[r:REACTED]-(profile:Profile)
    RETURN r, profile.id AS profileId, post.id AS postId";

        var result = await graphContext.ExecuteQueryAsync(query);

        foreach (var record in result)
        {
            var reaction = new Reaction();
            var parsedDictionary = record["r"].As<IRelationship>().Properties
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            parsedDictionary.Add("profileId", record["profileId"].ToString()!);
            parsedDictionary.Add("postId", record["postId"].ToString()!);
            reaction.MapToEntityFromNeo4j(parsedDictionary);

            reactions.Add(reaction);
        }

        return reactions;
    }

    /// <summary>
    ///     Método que deleta uma reação em um post
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