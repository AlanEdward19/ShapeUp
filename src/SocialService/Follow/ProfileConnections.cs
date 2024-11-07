using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialService.Follow;

/// <summary>
/// Classe que representa a conexão de um perfil com outros perfis.
/// </summary>
public class ProfileConnections
{
    /// <summary>
    /// Identificador único do documento.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    /// <summary>
    /// Id do perfil.
    /// </summary>
    [BsonElement("profileId")] public string ProfileId { get; set; }
    
    /// <summary>
    /// Lista de seguidores.
    /// </summary>
    [BsonElement("followers")] public List<string> Followers { get; set; } = new();
    
    /// <summary>
    /// Lista de perfis seguidos.
    /// </summary>
    [BsonElement("following")] public List<string> Following { get; set; } = new();

    /// <summary>
    /// Construtor da classe.
    /// </summary>
    /// <param name="profileId"></param>
    public ProfileConnections(Guid profileId)
    {
        ProfileId = profileId.ToString();
    }
}