using SocialService.Post.Comment.CommentOnPost;

namespace SocialService.Post.Comment;

/// <summary>
///     Classe que representa um comentário
/// </summary>
public class Comment : GraphEntity
{
    /// <summary>
    ///     Construtor padrão
    /// </summary>
    public Comment()
    {
    }

    /// <summary>
    ///     Construtor para criar um novo comentário.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="profileId"></param>
    public Comment(CommentOnPostCommand command, Guid profileId)
    {
        Id = Guid.NewGuid();
        ProfileId = profileId;
        PostId = command.PostId;
        UpdateContent(command.Content);

        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Id do perfil que fez o comentário
    /// </summary>
    public Guid ProfileId { get; private set; }

    /// <summary>
    ///     Id do post
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    ///     Data de criação do comentário
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Conteúdo do comentário
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    ///     Método para mapear os dados do neo4j para a entidade
    /// </summary>
    /// <param name="result"></param>
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        ProfileId = Guid.Parse(result["profileId"].ToString());
        Content = result["content"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());

        base.MapToEntityFromNeo4j(result);
    }

    /// <summary>
    ///     Método para atualizar o conteúdo do comentário.
    /// </summary>
    /// <param name="content"></param>
    public void UpdateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("O conteúdo não pode ser vazio.");

        if (Content != content)
            Content = content;
    }
}