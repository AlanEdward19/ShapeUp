namespace SocialService.Post.DeleteReactionFromPost;

/// <summary>
///     Comando para deletar uma reação de um post
/// </summary>
public class DeleteReactionFromPostCommand
{
    /// <summary>
    ///     Id do post
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    ///     Método para set o id do post
    /// </summary>
    /// <param name="postId"></param>
    public void SetPostId(Guid postId)
    {
        PostId = postId;
    }
}