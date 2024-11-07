using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialService.Friends;

/// <summary>
/// Classe que representa um perfil de amigo.
/// </summary>
public class Friend
{
    /// <summary>
    /// Construtor padrão.
    /// </summary>
    /// <param name="profileId"></param>
    public Friend(Guid profileId)
    {
        ProfileId = profileId.ToString();
    }

    /// <summary>
    /// Id do documento.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Id do perfil.
    /// </summary>
    [BsonElement("profileId")] public string ProfileId { get; set; }

    /// <summary>
    /// Lista de amigos.
    /// </summary>
    [BsonElement("friends")] public List<Friendship> Friends { get; set; } = new();
    
    /// <summary>
    /// Lista de convites enviados.
    /// </summary>
    [BsonElement("invitesSent")] public List<FriendshipInvite> InvitesSent { get; set; } = new();
    
    /// <summary>
    /// Lista de convites recebidos.
    /// </summary>
    [BsonElement("invitesReceived")] public List<FriendshipInvite> InvitesReceived { get; set; } = new();
}