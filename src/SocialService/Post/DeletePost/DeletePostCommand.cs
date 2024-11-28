namespace SocialService.Post.DeletePost;

/// <summary>
/// Comando para deletar um post
/// </summary>
public class DeletePostCommand
{
    /// <summary>
    /// Id do post
    /// </summary>
    public Guid PostId { get; private set; }
    
    /// <summary>
    /// Método para setar o id do post
    /// </summary>
    /// <param name="postId"></param>
    public void SetPostId(Guid postId) => PostId = postId;
}