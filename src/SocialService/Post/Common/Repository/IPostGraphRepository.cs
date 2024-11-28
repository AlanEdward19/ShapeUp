using SocialService.Post.CreatePost;

namespace SocialService.Post.Common.Repository;

/// <summary>
/// Interface do repositório de grafo para post
/// </summary>
public interface IPostGraphRepository
{
    /// <summary>
    /// Método que verifica se um post existe
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<bool> PostExistsAsync(Guid postId);
    
    /// <summary>
    /// Método que cria um post
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="postId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    Task CreatePostAsync(Guid profileId, Guid postId, CreatePostCommand command);
    
    /// <summary>
    /// Método que adiciona imagens a um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="images"></param>
    /// <returns></returns>
    Task UploadPostImagesAsync(Guid postId, List<Guid> images);
}