using SocialService.Post.Common.Enums;

namespace SocialService.Post.EditPost;

/// <summary>
///     Comando para criação de post.
/// </summary>
public class EditPostCommand
{
    /// <summary>
    ///     Id do post
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    ///     Conteúdo do post.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    ///     Visibilidade do post.
    /// </summary>
    public EPostVisibility? Visibility { get; set; }

    /// <summary>
    ///     Método para setar o id do post
    /// </summary>
    /// <param name="postId"></param>
    public void SetPostId(Guid postId)
    {
        PostId = postId;
    }
}