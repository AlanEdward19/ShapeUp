using SocialService.Post.CommentsOnPost;
using SocialService.Post.Common.Enums;
using SocialService.Post.CreatePost;
using SocialService.Post.EditCommentOnPost;
using SocialService.Post.GetPostComments;
using SocialService.Post.GetReactionsOnPost;

namespace SocialService.Post.Common.Repository;

/// <summary>
/// Interface do repositório de grafo para post
/// </summary>
public interface IPostGraphRepository
{
    #region Post

    /// <summary>
    /// Método que retorna um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<Post> GetPostAsync(Guid postId);
    
    /// <summary>
    /// Método que verifica se um post existe
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<bool> PostExistsAsync(Guid postId, Guid profileId);

    /// <summary>
    /// Método que cria um post
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="postId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<Post> CreatePostAsync(Guid profileId, Guid postId,  CreatePostCommand command);
    
    /// <summary>
    /// Método que atualiza um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<Post> UpdatePostAsync(EditPostCommand command);
    
    /// <summary>
    /// Método que adiciona imagens a um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="images"></param>
    /// <returns></returns>
    Task UploadPostImagesAsync(Guid postId, List<Guid> images);

    /// <summary>
    /// Método que deleta um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task DeletePostAsync(Guid postId);

    #endregion
    
    #region Comment

    /// <summary>
    /// Método que comenta em um post
    /// </summary>
    /// <param name="command"></param>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task CommentOnPostAsync(CommentOnPostCommand command, Guid profileId);
    
    /// <summary>
    /// Método que retorna os comentários de um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<IEnumerable<Comment>> GetPostCommentsAsync(Guid postId);
    
    /// <summary>
    /// Método que edita um comentário
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<Comment> EditCommentOnPostAsync(EditCommentOnPostCommand command);

    /// <summary>
    /// Método que deleta um comentário
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    Task DeleteCommentOnPostAsync(Guid commentId);

    #endregion
    
    #region React

    /// <summary>
    /// Método que adiciona uma reação a um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="profileId"></param>
    /// <param name="reactionType"></param>
    /// <returns></returns>
    Task ReactToPostAsync(Guid postId, Guid profileId, EReactionType reactionType);
    
    /// <summary>
    /// Método que retorna as reações de um post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    Task<IEnumerable<Reaction>> GetReactionsOnPostAsync(Guid postId);

    /// <summary>
    /// Método que deleta uma reação em um post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task DeleteReactionOnPostAsync(Guid postId, Guid profileId);

    #endregion
}