using MongoDB.Bson.Serialization.Attributes;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends;

public class Friendship
{
    public Friendship(string friendId, EFriendStatus status, DateTime requestedAt, DateTime? acceptedAt)
    {
        FriendId = friendId;
        Status = status;
        RequestedAt = requestedAt;
        AcceptedAt = acceptedAt;
    }

    [BsonElement("friendId")] public string FriendId { get; set; }

    [BsonElement("status")] public EFriendStatus Status { get; set; }

    [BsonElement("requestedAt")] public DateTime RequestedAt { get; set; }

    [BsonElement("acceptedAt")] public DateTime? AcceptedAt { get; set; }
}