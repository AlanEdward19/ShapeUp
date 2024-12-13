using SocialService.Common.Entities;

namespace SocialService.Post.Comment;

/// <summary>
///     Classe que representa um comentário
/// </summary>
public class Comment : GraphEntity
{
    /// <summary>
    ///     Id do perfil que fez o comentário
    /// </summary>
    public Guid ProfileId { get; private set; }

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
}