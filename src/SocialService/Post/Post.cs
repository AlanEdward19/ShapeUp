namespace SocialService.Post;

/// <summary>
/// Classe que representa um post.
/// </summary>
public class Post
{
    /// <summary>
    /// Id do post.
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    /// Construtor da classe.
    /// </summary>
    /// <param name="postId"></param>
    public Post(Guid postId)
    {
        PostId = postId;
    }
}