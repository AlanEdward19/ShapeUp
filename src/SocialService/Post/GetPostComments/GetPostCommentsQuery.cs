namespace SocialService.Post.GetPostComments;

/// <summary>
///     Query para obter os comentários de um post
/// </summary>
public class GetPostCommentsQuery
{
    /// <summary>
    ///     Id do post
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    ///     Método para setar o id do post
    /// </summary>
    /// <param name="postId"></param>
    public void SetPostId(Guid postId)
    {
        PostId = postId;
    }
}