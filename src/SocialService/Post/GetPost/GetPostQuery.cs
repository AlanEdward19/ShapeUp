namespace SocialService.Post.GetPost;

/// <summary>
///     Query para buscar um post
/// </summary>
public class GetPostQuery
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