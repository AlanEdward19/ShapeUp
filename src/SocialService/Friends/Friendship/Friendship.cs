using SocialService.Common.Entities;

namespace SocialService.Friends.Friendship;

/// <summary>
///     Classe que representa uma amizade entre dois perfis
/// </summary>
public class Friendship : GraphEntity
{
    /// <summary>
    ///     Construtor padrão
    /// </summary>
    /// <param name="profileAId"></param>
    /// <param name="profileBId"></param>
    public Friendship(Guid profileAId, Guid profileBId)
    {
        ProfileAId = profileAId.ToString();
        ProfileBId = profileBId.ToString();
    }

    /// <summary>
    ///     Id do perfil A
    /// </summary>
    public string ProfileAId { get; private set; }

    /// <summary>
    ///     Id do perfil B
    /// </summary>
    public string ProfileBId { get; set; }

    /// <summary>
    ///     Data de criação da amizade
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     Método para setar as informações da amizade
    /// </summary>
    /// <param name="result"></param>
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        ProfileAId = result["profileAId"].ToString();
        ProfileBId = result["profileBId"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());

        base.MapToEntityFromNeo4j(result);
    }
}