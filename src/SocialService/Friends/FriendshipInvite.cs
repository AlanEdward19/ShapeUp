using MongoDB.Bson.Serialization.Attributes;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends;

/// <summary>
/// Classe que representa uma solicitacao de amizade
/// </summary>
public class FriendshipInvite
{
    /// <summary>
    /// Construtor padrao
    /// </summary>
    /// <param name="friendId"></param>
    /// <param name="requestMessage"></param>
    public FriendshipInvite(string friendId, string? requestMessage)
    {
        FriendId = friendId;
        RequestMessage = requestMessage;
        RequestedAt = DateTime.Now;
    }

    /// <summary>
    /// Id do amigo
    /// </summary>
    [BsonElement("friendId")] public string FriendId { get; set; }
    
    /// <summary>
    /// Mensagem de solicitacao
    /// </summary>
    [BsonElement("requestMessage")]public string? RequestMessage { get; set; }
    
    /// <summary>
    /// Data em que a solicitacao foi feita
    /// </summary>
    [BsonElement("requestedAt")] public DateTime RequestedAt { get; set; }
}