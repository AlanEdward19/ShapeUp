using MongoDB.Bson.Serialization.Attributes;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends;

public class FriendshipInvite
{
    public FriendshipInvite(string friendId, string? requestMessage)
    {
        FriendId = friendId;
        RequestMessage = requestMessage;
        RequestedAt = DateTime.Now;
    }

    [BsonElement("friendId")] public string FriendId { get; set; }
    [BsonElement("requestMessage")]public string? RequestMessage { get; set; }
    [BsonElement("requestedAt")] public DateTime RequestedAt { get; set; }
}