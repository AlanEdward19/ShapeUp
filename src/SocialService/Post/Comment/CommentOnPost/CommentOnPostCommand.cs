namespace SocialService.Post.Comment.CommentOnPost;

/// <summary>
///     Comando para comentar em um post
/// </summary>
public class CommentOnPostCommand
{
    /// <summary>
    ///     Id do post
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    ///     Conteúdo do comentário
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Método para setar o id do post
    /// </summary>
    /// <param name="postId"></param>
    public void SetPostId(Guid postId)
    {
        PostId = postId;
    }
}