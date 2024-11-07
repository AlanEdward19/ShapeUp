using MongoDB.Bson.Serialization.Attributes;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends;

/// <summary>
/// Classe que representa uma amizade entre dois perfis
/// </summary>
public class Friendship
{
    /// <summary>
    /// Construtor padrao
    /// </summary>
    /// <param name="friendId"></param>
    /// <param name="acceptedAt"></param>
    public Friendship(string friendId, DateTime acceptedAt)
    {
        FriendId = friendId;
        AcceptedAt = acceptedAt;
    }

    /// <summary>
    /// Id do amigo
    /// </summary>
    [BsonElement("friendId")] public string FriendId { get; set; }

    /// <summary>
    /// Data em que a amizade foi aceita
    /// </summary>
    [BsonElement("acceptedAt")] public DateTime? AcceptedAt { get; set; }
}