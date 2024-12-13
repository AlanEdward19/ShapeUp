using SocialService.Post.Common.Enums;

namespace SocialService.Post;

/// <summary>
/// Objeto de transferência de dados para post
/// </summary>
/// <param name="post"></param>
public class PostDto(Post post)
{
    /// <summary>
    ///     Visibilidade do post.
    /// </summary>
    public EPostVisibility Visibility { get; private set; } = post.Visibility;

    /// <summary>
    /// Ids das imagens do post.
    /// </summary>
    public IEnumerable<Guid> Images { get; private set; } = post.Images;

    /// <summary>
    ///     Conteúdo do post.
    /// </summary>
    public string Content { get; private set; } = post.Content;
}