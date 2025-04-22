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
    public string PublisherId { get; private set; } = post.PublisherId;
    
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
    ///     Urls das imagens do post.
    /// </summary>
    public List<string> Images { get; private set; } = post.Images.ToList();

    /// <summary>
    ///     Conteúdo do post.
    /// </summary>
    public string Content { get; private set; } = post.Content;

    /// <summary>
    /// Quantidade de comentários do post.
    /// </summary>
    public int CommentsCount { get; private set; } = post.CommentsCount;
    
    /// <summary>
    /// Quantidade de reações do post.
    /// </summary>
    public int ReactionsCount { get; private set; } = post.ReactionsCount;

    /// <summary>
    /// Reações mais comuns do post.
    /// </summary>
    public List<EReactionType> TopReactions { get; private set; } = post.TopReactions;
    
    /// <summary>
    /// Método para setar as imagens do post.
    /// </summary>
    /// <param name="images"></param>
    public void SetImages(IEnumerable<string> images) =>Images = images.ToList();
    
    /// <summary>
    /// Método para setar a url da imagem do perfil que publicou o post.
    /// </summary>
    /// <param name="imageUrl"></param>
    public void SetPublisherImageUrl(string imageUrl) => PublisherImageUrl = imageUrl;
}