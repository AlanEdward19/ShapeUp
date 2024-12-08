using SocialService.Post.Common.Enums;

namespace SocialService.Post.CreatePost;

/// <summary>
///     Comando para criação de post.
/// </summary>
public class CreatePostCommand
{
    /// <summary>
    ///     Conteúdo do post.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    ///     Visibilidade do post.
    /// </summary>
    public EPostVisibility Visibility { get; set; }
}