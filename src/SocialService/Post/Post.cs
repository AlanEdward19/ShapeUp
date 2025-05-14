using SocialService.Post.Common.Enums;
using SocialService.Post.CreatePost;

namespace SocialService.Post;

/// <summary>
///     Classe que representa um post.
/// </summary>
public class Post : GraphEntity
{
    /// <summary>
    ///     Construtor padrão
    /// </summary>
    public Post()
    {
    }

    /// <summary>
    ///     Construtor para criar um novo post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="profileId"></param>
    public Post(CreatePostCommand command, string profileId)
    {
        Id = Guid.NewGuid();
        PublisherId = profileId;
        PublisherFirstName = "";
        PublisherLastName = "";
        PublisherImageUrl = "";
        UpdateVisibility(command.Visibility);
        UpdateContent(command.Content);
        Images = [];
        ReactionsCount = 0;
        CommentsCount = 0;
        TopReactions = [];
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Id do perfil que publicou o post.
    /// </summary>
    public string PublisherId { get; private set; }
    
    /// <summary>
    /// Primeiro nome do perfil que publicou o post.
    /// </summary>
    public string PublisherFirstName { get; private set; }
    
    /// <summary>
    /// Sobrenome do perfil que publicou o post.
    /// </summary>
    public string PublisherLastName { get; private set; }
    
    /// <summary>
    /// Url da imagem do perfil que publicou o post.
    /// </summary>
    public string PublisherImageUrl { get; private set; }

    /// <summary>
    ///     Visibilidade do post.
    /// </summary>
    public EPostVisibility Visibility { get; private set; }

    /// <summary>
    ///     Data de criação do post.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Data de atualização do post.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    ///     Ids das imagens do post
    /// </summary>
    public IEnumerable<string> Images { get; private set; }

    /// <summary>
    ///     Conteúdo do post.
    /// </summary>
    public string Content { get; private set; }
    
    /// <summary>
    /// Quantidade de reações do post.
    /// </summary>
    public int ReactionsCount { get; private set; }
    
    /// <summary>
    /// Quantidade de comentários do post.
    /// </summary>
    public int CommentsCount { get; private set; }
    
    /// <summary>
    /// Reações mais comuns do post.
    /// </summary>
    public List<EReactionType> TopReactions { get; private set; }
    
    /// <summary>
    /// Se o usuário reagiu ao post.
    /// </summary>
    public bool HasUserReacted { get; private set; }
    
    /// <summary>
    /// Se o usuário comentou no post.
    /// </summary>
    public bool HasUserCommented { get; private set; }

    /// <summary>
    ///     Método para mapear os dados do neo4j para a entidade.
    /// </summary>
    /// <param name="result"></param>
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Visibility = (EPostVisibility)Enum.Parse(typeof(EPostVisibility), result["visibility"].ToString()!);
        CreatedAt = DateTime.Parse(result["createdAt"].ToString()!);
        UpdatedAt = DateTime.Parse(result["updatedAt"].ToString()!);
        Content = result["content"].ToString()!;
        PublisherId = result["publisherId"].ToString()!;
        PublisherFirstName = result["publisherFirstName"].ToString()!;
        PublisherLastName = result["publisherLastName"].ToString()!;
        PublisherImageUrl = result["publisherImageUrl"].ToString()!;
        ReactionsCount = int.Parse(result["reactionsCount"].ToString()!);
        CommentsCount = int.Parse(result["commentsCount"].ToString()!);
        TopReactions = result["topReactions"].As<List<object>>()
            .Select(id => (EReactionType)Enum.Parse(typeof(EReactionType), id.ToString()!))
            .ToList();
        HasUserReacted = bool.Parse(result["hasUserReacted"].ToString()!);
        HasUserCommented = bool.Parse(result["hasUserCommented"].ToString()!);

        if (result.ContainsKey("images"))
            Images = result["images"] == null
                ? new List<string>()
                : ((List<object>)result["images"]).Select(id => id.ToString()!).ToList();

        else
            Images = new List<string>();

        base.MapToEntityFromNeo4j(result);
    }

    /// <summary>
    ///     Método para atualizar a visibilidade do post.
    /// </summary>
    /// <param name="visibility"></param>
    /// <param name="isUpdate"></param>
    public void UpdateVisibility(EPostVisibility? visibility, bool isUpdate = true)
    {
        if (visibility == null)
            return;

        if (!Enum.IsDefined(typeof(EPostVisibility), visibility))
            throw new ArgumentException("Valor de visibilidade inválido.");

        if (Visibility != visibility)
            Visibility = visibility.Value;

        if (isUpdate)
            UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Método para atualizar o conteúdo do post.
    /// </summary>
    /// <param name="content"></param>
    /// <param name="isUpdate"></param>
    public void UpdateContent(string? content, bool isUpdate = true)
    {
        if (string.IsNullOrWhiteSpace(content))
            return;

        if (Content != content)
            Content = content;

        if (isUpdate)
            UpdatedAt = DateTime.UtcNow;
    }
}