namespace SocialService.Post.Comment.DeleteCommentOnPost;

/// <summary>
///     Comando para deletar um comentário em um post
/// </summary>
public class DeleteCommentOnPostCommand
{
    /// <summary>
    ///     Id do comentário
    /// </summary>
    public Guid CommentId { get; private set; }

    /// <summary>
    ///     Método para setar o id do comentário
    /// </summary>
    /// <param name="commentId"></param>
    public void SetCommentId(Guid commentId)
    {
        CommentId = commentId;
    }
}