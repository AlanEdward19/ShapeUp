using SocialService.Post.Common.Enums;

namespace SocialService.Post.LikePost;

/// <summary>
/// Comando para reagir a um post.
/// </summary>
public class ReactToPostCommand
{
    /// <summary>
    /// Id do post
    /// </summary>
    public Guid PostId { get; private set; }
    
    /// <summary>
    /// Tipo de reação
    /// </summary>
    public EReactionType ReactionType { get; set; }
    
    /// <summary>
    /// Método para setar o id do post
    /// </summary>
    /// <param name="postId"></param>
    public void SetPostId(Guid postId) => PostId = postId;
}