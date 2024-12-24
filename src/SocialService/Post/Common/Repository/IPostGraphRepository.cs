using SocialService.Post.React;

namespace SocialService.Post.Common.Repository;

/// <summary>
///     Interface do repositório de grafo para post
/// </summary>
public interface IPostGraphRepository
{
    #region Post

    /// <summary>
    ///     Método que retorna um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<Post> GetPostAsync(Guid postId);

    /// <summary>
    ///     Método que verifica se um post existe
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<bool> PostExistsAsync(Guid postId);

    /// <summary>
    ///     Método que cria um post
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="postId"></param>
    /// <param name="post"></param>
    /// <returns></returns>
    Task CreatePostAsync(Guid profileId, Post post);

    /// <summary>
    ///     Método que atualiza um post
    /// </summary>
    /// <param name="post"></param>
    /// <returns></returns>
    Task UpdatePostAsync(Post post);

    /// <summary>
    ///     Método que adiciona imagens a um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="images"></param>
    /// <returns></returns>
    Task UploadPostImagesAsync(Guid postId, List<Guid> images);

    /// <summary>
    ///     Método que deleta um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task DeletePostAsync(Guid postId);

    #endregion

    #region Comment

    /// <summary>
    ///     Método que comenta em um post
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    Task CommentOnPostAsync(Comment.Comment comment);

    /// <summary>
    ///     Método que retorna os comentários de um post
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    Task<Comment.Comment> GetPostCommentsByCommentIdAsync(Guid commentId);

    /// <summary>
    ///     Método que retorna os comentários de um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<IEnumerable<Comment.Comment>> GetPostCommentsByPostIdAsync(Guid postId);

    /// <summary>
    ///     Método que edita um comentário
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    Task EditCommentOnPostAsync(Comment.Comment comment);

    /// <summary>
    ///     Método que deleta um comentário
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    Task DeleteCommentOnPostAsync(Guid commentId);

    /// <summary>
    ///     Método que verifica se um comentário existe
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    Task<bool> CommentExistsAsync(Guid commentId);

    #endregion

    #region React

    /// <summary>
    ///     Método que adiciona uma reação a um post
    /// </summary>
    /// <param name="reaction"></param>
    /// <returns></returns>
    Task ReactToPostAsync(Reaction reaction);

    /// <summary>
    ///     Método que retorna as reações de um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<IEnumerable<Reaction>> GetReactionsOnPostAsync(Guid postId);

    /// <summary>
    ///     Método que deleta uma reação em um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task DeleteReactionOnPostAsync(Guid postId, Guid profileId);

    #endregion
}