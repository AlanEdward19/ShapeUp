using Neo4j.Driver;
using SocialService.Database.Graph;
using SocialService.Post.CreatePost;

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
}