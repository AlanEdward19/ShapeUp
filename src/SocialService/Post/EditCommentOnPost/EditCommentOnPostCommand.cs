namespace SocialService.Post.EditCommentOnPost;

public class EditCommentOnPostCommand
{
    /// <summary>
    /// Id do comentário
    /// </summary>
    public Guid CommentId { get; private set; }
    
    /// <summary>
    /// Conteúdo do comentário
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Método para setar o id do comentário
    /// </summary>
    /// <param name="commentId"></param>
    public void SetCommentId(Guid commentId) => CommentId = commentId;
}