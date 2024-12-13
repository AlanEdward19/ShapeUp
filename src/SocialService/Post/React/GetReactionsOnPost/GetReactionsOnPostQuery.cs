namespace SocialService.Post.React.GetReactionsOnPost;

/// <summary>
///     Query para pegar reações em um post.
/// </summary>
public class GetReactionsOnPostQuery
{
    /// <summary>
    ///     Id do post.
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    ///     Método para setar o id do post.
    /// </summary>
    /// <param name="postId"></param>
    public void SetPostId(Guid postId)
    {
        PostId = postId;
    }
}