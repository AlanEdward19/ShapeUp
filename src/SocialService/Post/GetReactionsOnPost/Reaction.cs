using SocialService.Common.Entities;

namespace SocialService.Post.GetReactionsOnPost;

/// <summary>
///     Classe que representa uma reação
/// </summary>
public class Reaction : GraphEntity
{
    /// <summary>
    ///     Id do perfil que reagiu
    /// </summary>
    public Guid ProfileId { get; private set; }

    /// <summary>
    ///     Data de criação da reação
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Tipo da reação
    /// </summary>
    public string ReactionType { get; private set; }

    /// <summary>
    ///     Método para mapear os dados do neo4j para a entidade
    /// </summary>
    /// <param name="result"></param>
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        ProfileId = Guid.Parse(result["profileId"].ToString());
        ReactionType = result["type"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());

        base.MapToEntityFromNeo4j(result);
    }
}