using SocialService.Post.Common.Enums;

namespace SocialService.Post;

/// <summary>
///     Objeto de transferência de dados para post
/// </summary>
/// <param name="post"></param>
public class PostDto(Post post)
{
    /// <summary>
    /// Id do post.
    /// </summary>
    public Guid Id { get; private set; } = post.Id;
    
    /// <summary>
    /// Id do perfil que publicou o post.
    /// </summary>
    public Guid PublisherId { get; private set; } = post.PublisherId;
    
    /// <summary>
    /// Primeiro nome do perfil que publicou o post.
    /// </summary>
    public string PublisherFirstName { get; private set; } = post.PublisherFirstName;
    
    /// <summary>
    /// Sobrenome do perfil que publicou o post.
    /// </summary>
    public string PublisherLastName { get; private set; } = post.PublisherLastName;
    
    /// <summary>
    /// Url da imagem do perfil que publicou o post.
    /// </summary>
    public string PublisherImageUrl { get; private set; } = post.PublisherImageUrl;
    
    /// <summary>
    ///     Visibilidade do post.
    /// </summary>
    public EPostVisibility Visibility { get; private set; } = post.Visibility;

    /// <summary>
    ///     Ids das imagens do post.
    /// </summary>
    public IEnumerable<Guid> Images { get; private set; } = post.Images;

    /// <summary>
    ///     Conteúdo do post.
    /// </summary>
    public string Content { get; private set; } = post.Content;
}